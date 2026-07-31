using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace BWW.ScriptableObjects.Rendering
{
    public class ScriptableOutlineRendererFeature : ScriptableRendererFeature
    {
        [SerializeField] private SerializableOutlineSettings m_settings = new();

        private ScriptableOutlinePass m_pass;

        public override void Create()
        {
            m_pass = new ScriptableOutlinePass(m_settings);
        }

        public override void AddRenderPasses(ScriptableRenderer p_renderer, ref RenderingData p_renderingData)
        {
            if (m_settings.YellowMaskMaterial == null
            || m_settings.BlueMaskMaterial == null
            || m_settings.CompositeMaterial == null)
            {
                return;
            }

            // Pour éviter l'effet sur les previews de matériaux, etc.
            if (p_renderingData.cameraData.cameraType != CameraType.Game)
            {
                return;
            }

            p_renderer.EnqueuePass(m_pass);
        }
    }
}