using BWW.Enums;
using BWW.Utils.Characters;
using BWW.Utils.Map;
using UnityEngine;

namespace BWW.Behaviours.Characters
{
   public class VillagerAppearanceBehaviour : MonoBehaviour
   {
      private const string m_sShaderTexturePropertyName = "_DetailAlbedoMap";

      private bool m_bIsCharacterFemale;

      public bool IsCharacterFemale
      {
         get => m_bIsCharacterFemale;
      }

      private EVillagerTitle m_eTitle;

      public EVillagerTitle Title
      {
         get => m_eTitle;
      }

      private bool m_bIsSkinColorBlack;

      public bool IsSkinColorBlack
      {
         get => IsSkinColorBlack;
      }

      public void UpdateAppearance(bool p_bIsCharacterFemale)
      {
         m_bIsCharacterFemale = p_bIsCharacterFemale;

         VillageData l_data = CurrentGameVillagersDatabase.Instance.Village;

         m_eTitle = EVillagerTitle.POOR;

         for (int l_i = 1; l_i < 3; l_i++)
         {
            int l_dCurrentTitleCount = l_data.VillagerTitleCount[(EVillagerTitle) l_i];

            int l_dLastTitleCount = l_data.VillagerTitleCount[m_eTitle];

            if (l_dCurrentTitleCount < l_dLastTitleCount)
            {
               m_eTitle = (EVillagerTitle) l_i;
            }
         }

         m_bIsSkinColorBlack = true;

         int l_dCurrentColorCount = l_data.AreSkinColorBlackCount[false];

         int l_dLastColorCount = l_data.AreSkinColorBlackCount[m_bIsSkinColorBlack];

         if (l_dCurrentColorCount < l_dLastColorCount)
         {
            m_bIsSkinColorBlack = false;
         }

         string l_sTextureKey = string.Concat("skin_h", m_bIsCharacterFemale ? "er_" : "im_", m_eTitle.ToString().ToLower(), m_bIsSkinColorBlack ? "_1" : "_0");

         Texture2D l_text = VillagerTextureGetter.Instance.GetTextureFromKey(l_sTextureKey);

         l_data.VillagerTitleCount[m_eTitle] += 1;

         l_data.AreSkinColorBlackCount[m_bIsSkinColorBlack] += 1;

         l_data.AreVillagerWomenCount[m_bIsCharacterFemale] += 1;

         SkinnedMeshRenderer l_renderer = GetComponentInChildren<SkinnedMeshRenderer>();

         Material l_material = l_renderer.sharedMaterial;

         l_material.SetTexture(m_sShaderTexturePropertyName, l_text);

         l_renderer.material = l_material;
      }
   }
}
