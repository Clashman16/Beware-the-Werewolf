using BWW.Enums;
using UnityEngine;

namespace BWW.Behaviours.Characters
{
    public class VillagerDataBehaviour : CharacterDataBehaviour
    {
        EVillagerType m_eType;

        public EVillagerType Type
        {
            get => m_eType;
            set => m_eType = value;
        }

        public void Init(EVillagerType p_eVillagerType)
        {
            m_eType = p_eVillagerType;

            Init();
        }
    }
}
