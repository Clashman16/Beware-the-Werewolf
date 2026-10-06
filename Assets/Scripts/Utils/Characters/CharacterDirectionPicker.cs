using BWW.Behaviours.Map;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BWW.Utils.Characters
{
    public class CharacterDirectionPicker : PickingUtility
    {
        private NavMeshAgent m_agent;

        private float m_fMinimalDistance;

        private Dictionary<int, float> m_lstDistancesWithObstacles;

        private LayerMask m_obstacleLayerMask;

        public CharacterDirectionPicker(NavMeshAgent p_agent, LayerMask p_obstacleLayerMask) : base()
        {
            m_agent = p_agent;

            m_fMinimalDistance = Object.FindFirstObjectByType<GridCellBehaviour>().GetComponent<SpriteRenderer>().bounds.size.x;

            m_obstacleLayerMask = p_obstacleLayerMask;

            for (int l_i = 0; l_i < 3; l_i ++)
            {
                PossiblePicks.Add(l_i);
            }

            m_lstDistancesWithObstacles = new Dictionary<int, float>();
        }

        private Vector3 GetDirectionFromPick(int p_lastPick)
        {
            if(p_lastPick % 2 == 0)
            {
                return Quaternion.AngleAxis(
                45 * (-1 * p_lastPick / 2),
                Vector3.up
            ) * -m_agent.transform.forward;
            }
            else
            {
                return - m_agent.transform.forward;
            }
        }

        private bool CheckCollisionWithDirection(Vector3 p_vecDirection, float p_fDistance, out RaycastHit p_hit)
        {
            p_vecDirection.y = 0f;
            p_vecDirection.Normalize();

            Vector3 l_vecCenter = m_agent.transform.position + Vector3.up * (m_agent.height * 0.5f);

            Vector3 l_vecHalfExtents = new Vector3(m_agent.radius, m_agent.height * 0.5f, 0.01f);

            Quaternion l_rotation = Quaternion.LookRotation(p_vecDirection, Vector3.up);

            return Physics.BoxCast(
                            l_vecCenter,
                            l_vecHalfExtents,
                            p_vecDirection,
                            out p_hit,
                            l_rotation,
                            p_fDistance,
                            m_obstacleLayerMask,
                            QueryTriggerInteraction.Collide
            );
        }

        private void ResetPicker()
        {
            PossiblePicks.Clear();

            for (int l_i = 0; l_i < 3; l_i++)
            {
                PossiblePicks.Add(l_i);
            }

            m_lstDistancesWithObstacles.Clear();

            LastPickedId = -1;
        }

        private (Vector3, float) PrecisePick(int p_dPickId, float p_fDistance)
        {
            m_lstDistancesWithObstacles.Clear();

            Vector3 l_vecDirection = GetDirectionFromPick(p_dPickId);

            bool l_bWillCollide = true;

            while (l_bWillCollide)
            {
                p_fDistance = p_fDistance / 2;

                l_bWillCollide = CheckCollisionWithDirection(l_vecDirection, p_fDistance, out RaycastHit p_hit);
            }

            return (l_vecDirection, p_fDistance);
        }

        public (Vector3, float) PickDestination()
        {
            bool l_bWillCollide = false;

            Vector3 l_vecDirection = Vector3.zero;

            while (LastPickedId == -1 || l_bWillCollide)
            {
                Pick();

                int l_dPick = PossiblePicks[LastPickedId];

                l_vecDirection = GetDirectionFromPick(l_dPick);

                l_bWillCollide = CheckCollisionWithDirection(l_vecDirection, m_fMinimalDistance, out RaycastHit p_hit);

                if(l_bWillCollide)
                {
                    m_lstDistancesWithObstacles.Add(l_dPick, p_hit.distance);

                    PossiblePicks.Remove(l_dPick);

                    if (m_lstDistancesWithObstacles.Count == 3)
                    {
                        l_bWillCollide = false;
                    }
                }
            }

            if (m_lstDistancesWithObstacles.Count > 0)
            {
                float l_fDistance = 100;

                int l_dPickId = -1;

                foreach (int l_i in m_lstDistancesWithObstacles.Keys)
                {
                    if (m_lstDistancesWithObstacles[l_i] < l_fDistance)
                    {
                        l_fDistance = m_lstDistancesWithObstacles[l_i];

                        l_dPickId = l_i;
                    }
                }

                ResetPicker();

                return PrecisePick(l_dPickId, l_fDistance);
            }
            else
            {
                Pick();

                int l_dPick = PossiblePicks[LastPickedId];

                l_vecDirection = GetDirectionFromPick(l_dPick);

                ResetPicker();

                return (l_vecDirection, m_fMinimalDistance);
            }
        }
    }
}
