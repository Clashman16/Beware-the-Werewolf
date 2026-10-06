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

            m_fBaseHealthPoints = m_fHealthPoints;
        }
    }
}
