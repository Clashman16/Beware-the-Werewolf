using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace BWW.ScriptableObjects.Rendering
{
    [System.Serializable]
    internal class SerializableOutlineSettings
    {
        [Header("Filtering")]

        [SerializeField] private LayerMask m_outlineLayer;
        public LayerMask OutlineLayer
        {
            get => m_outlineLayer;
        }

        [Header("Materials")]

        [SerializeField] private Material m_maskMaterial;

        public Material MaskMaterial
        {
            get => m_maskMaterial;
        }

        [SerializeField] private Material m_compositeMaterial;

        public Material CompositeMaterial
        {
            get => m_compositeMaterial;
        }

        [Header("Rendering")]

        [SerializeField] private RenderPassEvent m_renderPassEvent = RenderPassEvent.AfterRenderingOpaques;

        public RenderPassEvent RenderPassEvent
        {
            get => m_renderPassEvent;
        }
    }
}