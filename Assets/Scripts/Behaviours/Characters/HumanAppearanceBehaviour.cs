using BWW.Utils.Characters;
using UnityEngine;

namespace BWW.Behaviours.Characters
{
    public abstract class HumanAppearanceBehaviour : MonoBehaviour
    {
        internal const string m_sShaderTexturePropertyName = "_DetailAlbedoMap";

        private bool m_bIsCharacterFemale;

        public bool IsCharacterFemale
        {
            get => m_bIsCharacterFemale;
            set => m_bIsCharacterFemale = value;
        }

        public abstract void UpdateAppearance(bool p_bIsCharacterFemale);

        internal Texture2D GetTextureFromKey(string p_sTextureKey)
        {
            return VillagerTextureGetter.Instance.GetTextureFromKey(p_sTextureKey);
        }

        internal void ApplyTexture(Texture2D p_newTexture)
        {
            SkinnedMeshRenderer l_renderer = GetComponentInChildren<SkinnedMeshRenderer>();

            Material l_material = new Material(l_renderer.sharedMaterial);

            l_material.SetTexture(m_sShaderTexturePropertyName, p_newTexture);

            l_renderer.material = l_material;
        }
    }
}
