using BWW.Enums;
using UnityEngine;

namespace BWW.Behaviours.Characters
{
   public class VillagerAppearanceBehaviour : MonoBehaviour
   {
      bool m_bIsCharacterFemale;

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
      }
   }
}
