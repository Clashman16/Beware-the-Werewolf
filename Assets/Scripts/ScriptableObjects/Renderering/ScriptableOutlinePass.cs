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

        private class MaskPassData
        {
            public RendererListHandle rendererList;
        }

        private class CompositePassData
        {
            public Material material;
        }

        internal ScriptableOutlinePass(SerializableOutlineSettings p_settings)
        {
            m_settings = p_settings;

            renderPassEvent = p_settings.RenderPassEvent;

            ConfigureInput(ScriptableRenderPassInput.Depth);
        }

        public override void RecordRenderGraph(RenderGraph p_renderGraph, ContextContainer p_frameContext)
        {
            UniversalResourceData l_resourceData = p_frameContext.Get<UniversalResourceData>();

            UniversalRenderingData l_renderingData = p_frameContext.Get<UniversalRenderingData>();

            UniversalCameraData l_cameraData =  p_frameContext.Get<UniversalCameraData>();

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
            l_descriptor.colorFormat = RenderTextureFormat.R8;

            TextureHandle maskTexture =
                UniversalRenderer.CreateRenderGraphTexture(
                    p_renderGraph,
                    l_descriptor,
                    "_OutlineMaskTexture",
                    false
                );

            /*
             * --------------------------------------
             * 2. Pass masque
             * --------------------------------------
             */

            using (
                var l_builder =
                    p_renderGraph.AddRasterRenderPass<MaskPassData>(
                        "BWW Outline Mask",
                        out MaskPassData p_passData
                    )
            )
            {
                SortingCriteria l_sortingCriteria = l_cameraData.defaultOpaqueSortFlags;

                FilteringSettings filteringSettings =
                    new FilteringSettings(
                        RenderQueueRange.opaque,
                        m_settings.OutlineLayer
                    );

                ShaderTagId l_shaderTag = new ShaderTagId("UniversalForward");

                DrawingSettings l_drawingSettings =
                    RenderingUtils.CreateDrawingSettings(
                        l_shaderTag,
                        l_renderingData,
                        l_cameraData,
                        l_lightData,
                        l_sortingCriteria
                    );

                // Supporte aussi certains shaders URP alternatifs.
                l_drawingSettings.SetShaderPassName(1, new ShaderTagId("UniversalForwardOnly"));

                l_drawingSettings.SetShaderPassName(2, new ShaderTagId("SRPDefaultUnlit"));

                l_drawingSettings.overrideMaterial = m_settings.MaskMaterial;

                // Le masque n'a besoin d'aucune donnée d'éclairage.
                l_drawingSettings.perObjectData = PerObjectData.None;

                // On utilise explicitement le premier pass du matériau de masque.
                l_drawingSettings.overrideMaterialPassIndex = 0;

                RendererListParams rendererListParams =
                    new RendererListParams(
                        l_renderingData.cullResults,
                        l_drawingSettings,
                        filteringSettings
                    );

                p_passData.rendererList = p_renderGraph.CreateRendererList(rendererListParams);

                l_builder.UseRendererList(p_passData.rendererList);

                // Le masque est notre destination couleur.
                l_builder.SetRenderAttachment(maskTexture, 0);

                /*
                 * On lit le depth buffer de la caméra.
                 *
                 * Ainsi, un objet caché derrière un mur
                 * ne produit pas de contour à travers le mur.
                 */
                l_builder.SetRenderAttachmentDepth(l_resourceData.cameraDepthTexture, AccessFlags.Read);

                /*
                 * Rend le masque accessible au pass suivant
                 * sous le nom _OutlineMaskTexture.
                 */
                l_builder.SetGlobalTextureAfterPass(maskTexture, m_dOutlineMaskId);

                l_builder.SetRenderFunc(
                    (
                        MaskPassData p_data,
                        RasterGraphContext p_context) =>
                    {
                        // Fond du masque = noir.
                        p_context.cmd.ClearRenderTarget(
                            false,
                            true,
                            Color.black
                        );

                        // Objets sélectionnés = blanc.
                        p_context.cmd.DrawRendererList(
                            p_data.rendererList
                        );
                    }
                );
            }

            /*
             * --------------------------------------
             * 3. Pass du contour
             * --------------------------------------
             */

            using (
                var l_builder =
                    p_renderGraph.AddRasterRenderPass<CompositePassData>(
                        "BWW Outline Composite",
                        out CompositePassData p_passData
                    )
            )
            {
                p_passData.material = m_settings.CompositeMaterial;

                /*
                 * Indique explicitement à Render Graph
                 * que ce pass lit notre texture globale.
                 */
                l_builder.UseGlobalTexture(m_dOutlineMaskId, AccessFlags.Read);

                /*
                 * Le résultat est dessiné directement
                 * par-dessus l'image de la caméra.
                 */
                l_builder.SetRenderAttachment(l_resourceData.activeColorTexture, 0);

                l_builder.SetRenderFunc(
                    (
                        CompositePassData p_data,
                        RasterGraphContext p_context) =>
                    {
                        /*
                         * Notre shader utilise SV_VertexID et
                         * génère un triangle plein écran.
                         */
                        p_context.cmd.DrawProcedural(
                            Matrix4x4.identity,
                            p_data.material,
                            0,
                            MeshTopology.Triangles,
                            3,
                            1
                        );
                    }
                );
            }
        }
    }
}