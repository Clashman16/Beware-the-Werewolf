using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

namespace BWW.ScriptableObjects.Rendering
{
    internal class ScriptableOutlinePass : ScriptableRenderPass
    {
        private readonly SerializableOutlineSettings m_settings;

        private static readonly int m_dOutlineMaskId = Shader.PropertyToID("_OutlineMaskTexture");

        internal ScriptableOutlinePass(SerializableOutlineSettings p_settings)
        {
            m_settings = p_settings;

            renderPassEvent = p_settings.RenderPassEvent;

            ConfigureInput(ScriptableRenderPassInput.Depth);
        }

        private RendererListHandle CreateRendererList(RenderGraph p_renderGraph, UniversalRenderingData p_renderingData, UniversalCameraData p_cameraData,
                                                      UniversalLightData p_lightData, LayerMask p_layerMask, Material p_overrideMaterial)
        {
            SortingCriteria l_sortingCriteria = p_cameraData.defaultOpaqueSortFlags;

            FilteringSettings l_filteringSettings = new FilteringSettings(RenderQueueRange.opaque,p_layerMask);

            ShaderTagId l_shaderTag = new ShaderTagId("UniversalForward");

            DrawingSettings l_drawingSettings = RenderingUtils.CreateDrawingSettings(
                    l_shaderTag,
                    p_renderingData,
                    p_cameraData,
                    p_lightData,
                    l_sortingCriteria
                );

            l_drawingSettings.SetShaderPassName(1, new ShaderTagId("UniversalForwardOnly"));

            l_drawingSettings.SetShaderPassName(2, new ShaderTagId("SRPDefaultUnlit"));

            l_drawingSettings.overrideMaterial = p_overrideMaterial;

            l_drawingSettings.overrideMaterialPassIndex = 0;

            l_drawingSettings.perObjectData = PerObjectData.None;

            RendererListParams l_rendererListParams = new RendererListParams(p_renderingData.cullResults,l_drawingSettings,l_filteringSettings);

            return p_renderGraph.CreateRendererList(l_rendererListParams);
        }

        public override void RecordRenderGraph(RenderGraph p_renderGraph, ContextContainer p_frameContext)
        {
            UniversalResourceData l_resourceData = p_frameContext.Get<UniversalResourceData>();

            UniversalRenderingData l_renderingData = p_frameContext.Get<UniversalRenderingData>();

            UniversalCameraData l_cameraData = p_frameContext.Get<UniversalCameraData>();

            UniversalLightData l_lightData = p_frameContext.Get<UniversalLightData>();

            /*
             * --------------------------------------
             * 1. Création de la texture masque
             * --------------------------------------
             */

            RenderTextureDescriptor l_descriptor = l_cameraData.cameraTargetDescriptor;

            l_descriptor.depthBufferBits = 0;
            l_descriptor.msaaSamples = 1;

            // Une seule composante nous suffit pour noir/blanc.
            l_descriptor.colorFormat = RenderTextureFormat.ARGB32;

            TextureHandle l_maskTexture = UniversalRenderer.CreateRenderGraphTexture( p_renderGraph, l_descriptor, "_OutlineMaskTexture", false);

            /*
             * --------------------------------------
             * 2. Pass masque
             * --------------------------------------
             */

            using (var l_builder = p_renderGraph.AddRasterRenderPass<MaskPassData>("BWW Multicolor Outline Mask", out MaskPassData p_passData))
            {
                p_passData.YellowRendererList =
                    CreateRendererList(
                        p_renderGraph,
                        l_renderingData,
                        l_cameraData,
                        l_lightData,
                        m_settings.YellowOutlineLayer,
                        m_settings.YellowMaskMaterial
                    );

                p_passData.BlueRendererList =
                    CreateRendererList(
                        p_renderGraph,
                        l_renderingData,
                        l_cameraData,
                        l_lightData,
                        m_settings.BlueOutlineLayer,
                        m_settings.BlueMaskMaterial
                    );

                l_builder.UseRendererList(p_passData.YellowRendererList);

                l_builder.UseRendererList(p_passData.BlueRendererList);

                // La comparaison de profondeur est faite dans le shader OutlineMask.
                l_builder.UseTexture(l_resourceData.cameraDepthTexture, AccessFlags.Read);

                l_builder.SetRenderAttachment(l_maskTexture, 0);

                l_builder.SetGlobalTextureAfterPass(l_maskTexture,m_dOutlineMaskId);

                l_builder.SetRenderFunc((
                        MaskPassData p_data,
                        RasterGraphContext p_context) =>
                    {
                        // Aucun objet = noir.
                        p_context.cmd.ClearRenderTarget(false, true,Color.black);

                        // Ces objets écrivent (1,0,0,1).
                        p_context.cmd.DrawRendererList(p_data.YellowRendererList);

                        // Ces objets écrivent (0,1,0,1).
                        p_context.cmd.DrawRendererList(p_data.BlueRendererList);
                    }
                );
            }

            /*
             * --------------------------------------
             * 3. Pass du contour
             * --------------------------------------
             */

            using (var l_builder = p_renderGraph.AddRasterRenderPass<CompositePassData>("BWW Outline Composite",out CompositePassData p_passData))
            {
                p_passData.Material = m_settings.CompositeMaterial;

                /*
                 * Indique explicitement à Render Graph que ce pass lit notre texture globale.
                 */
                l_builder.UseGlobalTexture(m_dOutlineMaskId, AccessFlags.Read);

                /*
                 * Le résultat est dessiné directement par-dessus l'image de la caméra.
                 */
                l_builder.SetRenderAttachment(l_resourceData.activeColorTexture, 0);

                l_builder.SetRenderFunc(
                    (
                        CompositePassData p_data,
                        RasterGraphContext p_context) =>
                    {
                        /*
                         * Notre shader utilise SV_VertexID et génère un triangle plein écran.
                         */
                        p_context.cmd.DrawProcedural(Matrix4x4.identity, p_data.Material, 0, MeshTopology.Triangles, 3, 1);
                    }
                );
            }
        }
    }
}