using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace BWW.Behaviours.Characters
{
   public class VillagerMovementBehaviour : MonoBehaviour
   {
      private NavMeshAgent m_agent;

      private Vector3 m_vecTarget;

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

         MoveToTarget();
      }

      public void MoveToTarget()
      {
         m_agent.SetDestination(m_vecTarget);
      }
   }
}
