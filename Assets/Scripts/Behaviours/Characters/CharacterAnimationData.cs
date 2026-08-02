using BWW.Enums;

namespace BWW.Behaviours.Characters
{
    public struct CharacterAnimationData
    {
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

        private bool m_bIsWounded;

        public bool IsWounded
        {
            get => m_bIsWounded;
            set => m_bIsWounded = value;
        }

        public CharacterAnimationData(ECharacterState p_eState, bool p_bIsBurnt, bool p_bIsWounded)
        {
            m_eState = p_eState;
            m_bIsBurnt = p_bIsBurnt;
            m_bIsWounded = p_bIsWounded;
        }
    }
}
