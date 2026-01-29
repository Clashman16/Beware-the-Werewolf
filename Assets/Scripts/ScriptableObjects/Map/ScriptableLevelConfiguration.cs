using System.Collections.Generic;
using UnityEngine;

namespace BWW.ScriptableObjects.Map
{
    [CreateAssetMenu(fileName = "ScriptableLevelConfiguration", menuName = "BWW/ScriptableObjects/ScriptableLevelConfiguration")]
    public class ScriptableLevelConfiguration : ScriptableObject
    {
        [SerializeField] private int m_dMainSpawnerCount;

        [SerializeField] private List<SwitchablePartCount> m_lstAllPossibleParts;

        [SerializeField] private List<ScriptableEnemyWave> m_lstWaves;
    }
}
