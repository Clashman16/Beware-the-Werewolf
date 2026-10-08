using BWW.Enums;

namespace BWW.Behaviours.Characters
{
    public class WerewolfDataBehaviour : CharacterDataBehaviour
    {
        EWerewolfType m_eType;

        public EWerewolfType Type
        {
            get => m_eType;
            set => m_eType = value;
        }

        public void Init(EWerewolfType p_eType)
        {
            m_eType = p_eType;

            HealthPoints = 150f;

            Init();
        }
    }
}
