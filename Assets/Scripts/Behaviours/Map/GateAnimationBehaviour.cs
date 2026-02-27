using BWW.Behaviours.Characters;
using UnityEngine;

namespace BWW.Behaviours.Map
{
   public class GateAnimationBehaviour : MonoBehaviour
   {
      private const string m_sAnimationKey = "IsOpen";

      private Animator m_animator;

      private VillagerMovementBehaviour m_villagerBehindGate;

      public VillagerMovementBehaviour VillagerBehindGate
      {
         set
         {
            m_villagerBehindGate = value;

            OpenGate(true);
         }
      }

      private Vector3 m_vecGatePosition;

      public Vector3 GatePosition
      {
         set => m_vecGatePosition = value;
      }

      private void Start()
      {
         m_animator = GetComponent<Animator>();
      }

      private void OpenGate(bool p_bIsOpen)
      {
         m_animator.SetBool(m_sAnimationKey, p_bIsOpen);
      }

      public void OnGateOpened()
      {
         if (m_villagerBehindGate != null)
         {
            m_villagerBehindGate.ResumeMove();
         }
      }

      private void Update()
      {
         if (m_villagerBehindGate != null)
         {
            if(m_animator.GetBool(m_sAnimationKey))
            {
               if (Vector3.Distance(m_vecGatePosition, m_villagerBehindGate.transform.position + Vector3.up * 0.5f) >= 1)
               {
                  OpenGate(false);

                  m_villagerBehindGate = null;
               }
            }
            
         }
      }
   }
}
