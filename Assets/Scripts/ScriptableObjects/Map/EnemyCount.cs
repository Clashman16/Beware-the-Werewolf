using BWW.Enums;
using System;
using UnityEngine;

namespace BWW.ScriptableObjects.Map
{
    [Serializable]
    public struct EnemyCount
    {
        [SerializeField] private EEnemyType m_ePart;

        [SerializeField] private int m_dCount;
    }
}
