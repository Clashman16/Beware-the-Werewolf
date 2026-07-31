using UnityEngine;

namespace BWW.ScriptableObjects.Rendering
{
    internal class CompositePassData
    {
        [SerializeField] Material m_material;

        public Material Material
        {
            get => m_material;
            set => m_material = value;
        }
    }
}