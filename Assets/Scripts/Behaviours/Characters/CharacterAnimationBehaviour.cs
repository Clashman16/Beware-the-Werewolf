using BWW.Enums;
using BWW.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace BWW.Behaviours.Characters
{
    public abstract class CharacterAnimationBehaviour : MonoBehaviour
    {
        private Animator m_animator;

        private CharacterDataBehaviour m_currentData;

        private CharacterAnimationData m_data;

        private UnityAction m_OnDataUpdated;

        private PickingUtility m_animationIndexPicker;

        public void Init(CharacterDataBehaviour p_currentData)
        {
            m_animator = GetComponent<Animator>();

            m_currentData = p_currentData;

            m_animationIndexPicker = new PickingUtility();

            for(int l_i = 0; l_i < 3; l_i++)
            {
                m_animationIndexPicker.PossiblePicks.Add(l_i);
            }

            m_OnDataUpdated += UpdateAnimation;

            ResetInstance();
        }

        private void ResetInstance()
        {
            m_data = new CharacterAnimationData(ECharacterState.IDDLE, false, false);

            m_animator.SetInteger("WalkIndex", m_animationIndexPicker.Pick());

            m_OnDataUpdated.Invoke();
        }

        private void Update()
        {
            bool l_bIsWounded = m_currentData.IsWounded;

            if (m_currentData.State != m_data.State || l_bIsWounded != m_data.IsWounded || m_currentData.IsBurnt != m_data.IsBurnt)
            {
                m_data.State = m_currentData.State;

                m_data.IsWounded = l_bIsWounded;

                m_data.IsBurnt = m_currentData.IsBurnt;

                m_OnDataUpdated.Invoke();
            }
        }

        private void UpdateAnimation()
        {
            m_animator.SetBool("WOUNDED", m_data.IsWounded);

            m_animator.SetBool("BURNT", m_data.IsBurnt);

            ECharacterState l_eCurrentState = m_data.State;

            int l_dStateCount = sizeof(ECharacterState);

            for (int l_i = 0; l_i < l_dStateCount ; l_i++)
            {
                ECharacterState l_eState = (ECharacterState) l_i;

                m_animator.SetBool(l_eState.ToString(), l_eState == l_eCurrentState);
            }

            if (l_eCurrentState == ECharacterState.ATTACKING)
            {
                LaunchAttackAnimation();
            }
            else
            {
                m_animator.SetInteger("DefaultAnimIndex", m_animationIndexPicker.Pick());
            }
        }

        public abstract void LaunchAttackAnimation();
    }
}
