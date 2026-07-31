using UnityEngine;
using UnityEngine.Rendering.RenderGraphModule;

namespace BWW.ScriptableObjects.Rendering
{
    internal class MaskPassData
    {
        [SerializeField]
        private RendererListHandle m_yellowRendererList;

        public RendererListHandle YellowRendererList
        {
            get => m_yellowRendererList;
            set => m_yellowRendererList = value;
        }

        [SerializeField]
        private RendererListHandle m_blueRendererList;

        public RendererListHandle BlueRendererList
        {
            get => m_blueRendererList;
            set => m_blueRendererList = value;
        }
    }
}
