using BWW.Enums;
using UnityEngine;

namespace BWW.Behaviours.Characters
{
   public class CharacterDataBehaviour : MonoBehaviour
   {
      private float m_fHealthPoints;

      public float HealthPoints
      {
         get => m_fHealthPoints;
         set => m_fHealthPoints = value;
      }

      private ECharacterState m_eState;

      public ECharacterState State
      {
         get => m_eState;
         set => m_eState = value;
      }

      private bool m_bIsBurnt;

      public bool IsBurnt
      {
         get => m_bIsBurnt;
         set => m_bIsBurnt = value;
      }

        private float m_fBaseHealthPoints;

        public bool IsWounded => m_fHealthPoints <= m_fBaseHealthPoints / 2;


      public void Init()
      {
            m_eState = ECharacterState.IDDLE;

         VillagerAppearanceBehaviour l_villager = GetComponent<VillagerAppearanceBehaviour>();

         if (l_villager != null)
         {
            switch(l_villager.Title)
            {
               case EVillagerTitle.KNIGHT:

                  m_fHealthPoints = 100f;

                  break;
               case EVillagerTitle.RICH:

                  m_fHealthPoints = 80f;

                  break;

               default: 
                  m_fHealthPoints = 50f;

                  break;
            }
         }
         else // This is the werewolf
         {
            m_fHealthPoints = 150f;
         }

            m_fBaseHealthPoints = m_fHealthPoints;
      }
   }
}
