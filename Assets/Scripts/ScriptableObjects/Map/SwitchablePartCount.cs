using BWW.Enums;
using System;
using UnityEngine;

namespace BWW.ScriptableObjects.Map
{
    [Serializable]
    public struct SwitchablePartCount
    {
        [SerializeField] private ESwitchablePart m_ePart;

        public ESwitchablePart Part
        {
            get => m_ePart;
        }

        [SerializeField] private int m_dCount;

        public int Count
        {
            get => m_dCount;
        }
    }
}
