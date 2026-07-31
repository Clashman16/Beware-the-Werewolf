using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace BWW.ScriptableObjects.Rendering
{
    [System.Serializable]
    internal class SerializableOutlineSettings
    {
        [Header("Filtering")]

        [SerializeField] private LayerMask m_yellowOutlineLayer;
        public LayerMask YellowOutlineLayer
        {
            get => m_yellowOutlineLayer;
        }

        [SerializeField] private LayerMask m_blueOutlineLayer;
        public LayerMask BlueOutlineLayer
        {
            get => m_blueOutlineLayer;
        }

        [Header("Materials")]

        [SerializeField] private Material m_yellowMaskMaterial;

        public Material YellowMaskMaterial
        {
            get => m_yellowMaskMaterial;
        }

        [SerializeField] private Material m_blueMaskMaterial;

        public Material BlueMaskMaterial
        {
            get => m_blueMaskMaterial;
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