using BWW.Enums;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace BWW.Behaviours.Characters
{
    public abstract class CharacterMovementBehaviour : MonoBehaviour
    {
        private NavMeshAgent m_agent;

        internal NavMeshAgent Agent
        {
            get => m_agent;
            set => m_agent = value;
        }

        private Vector3 m_vecTarget;

        internal Vector3 Target
        {
            get => m_vecTarget;
            set => m_vecTarget = value;
        }

        private NavMeshPath m_path;

        internal NavMeshPath Path
        {
            get => m_path;
            set => m_path = value;
        }

        private CharacterDataBehaviour m_data;

        private Vector3 m_vecPreviousPosition;

        internal Vector3 PreviousPosition
        {
            set => m_vecPreviousPosition = value;
        }

        public void Init(CharacterDataBehaviour p_data)
        {
            m_data = p_data;

            ResetInstance();
        }

        private void ResetInstance()
        {
            StartCoroutine(LaunchNavMeshAgent());
        }

        internal abstract IEnumerator LaunchNavMeshAgent();

        internal virtual void Update()
        {
            Vector3 l_vecCurrentPosition = transform.position;

            if (m_data.State != ECharacterState.PUSHED)
            {
                if (l_vecCurrentPosition != m_vecPreviousPosition)
                {
                    if (m_data.State != ECharacterState.WALKING)
                    {
                        m_data.State = ECharacterState.WALKING;
                    }

                    m_vecPreviousPosition = l_vecCurrentPosition;
                }
                else
                {
                    if (m_data.State != ECharacterState.IDDLE)
                    {
                        m_data.State = ECharacterState.IDDLE;
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
