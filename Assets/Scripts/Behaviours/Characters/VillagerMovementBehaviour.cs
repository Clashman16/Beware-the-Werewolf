using BWW.Behaviours.Map;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace BWW.Behaviours.Characters
{
   public class VillagerMovementBehaviour : MonoBehaviour
   {
      private NavMeshAgent m_agent;

      private Vector3 m_vecTarget;

      private NavMeshPath m_path;

      private void Start()
      {
         StartCoroutine(LaunchNavMeshAgent());
      }

      private IEnumerator LaunchNavMeshAgent()
      {
         m_agent = GetComponent<NavMeshAgent>();

         m_vecTarget = new Vector3(18.0540009f, 0, -34.9550018f);

         m_agent.enabled = false;

         NavMeshHit l_hit;

         while (!NavMesh.SamplePosition(transform.position, out l_hit, 10, NavMesh.AllAreas))
         {
            yield return null;
         }

         transform.position = l_hit.position;
         m_agent.enabled = true;

         PrepareToMove();
      }

      public void PrepareToMove()
      {
         m_path = new NavMeshPath();

         if(m_agent.CalculatePath(m_vecTarget, m_path))
         {
            Vector3 l_vecStartPosition = transform.position + Vector3.up * 0.5f;

            Vector3 l_vecNextPosition = m_path.corners[1];

            Vector3 l_vecDirection = (l_vecNextPosition - l_vecStartPosition).normalized;

            float l_fDistance = Vector3.Distance(l_vecNextPosition, l_vecStartPosition);

            int l_dLayerMaskId = LayerMask.GetMask("CastleGate");

            if (Physics.Raycast(l_vecStartPosition, l_vecDirection, out RaycastHit l_hit, l_fDistance, l_dLayerMaskId))
            {
               if (l_hit.collider != null)
               {
                  GateAnimationBehaviour l_gateAnimation = l_hit.collider.GetComponent<GateAnimationBehaviour>();

                  if (l_gateAnimation != null)
                  {
                     l_gateAnimation.GatePosition = l_hit.point;

                     l_gateAnimation.VillagerBehindGate = this;
                  }
               }
            }
         }
      }

      public void ResumeMove()
      {
         m_agent.SetPath(m_path);
      }
   }
}
