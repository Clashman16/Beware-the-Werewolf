using System.Collections.Generic;
using UnityEngine;

namespace BWW.ScriptableObjects.Map
{
    [CreateAssetMenu(fileName = "ScriptableLevelConfiguration", menuName = "BWW/ScriptableObjects/ScriptableLevelConfiguration")]
    public class ScriptableLevelConfiguration : ScriptableObject
    {
        [SerializeField] private int m_dMainSpawnerCount;

        public int MainSpawnerCount
        {
            get => m_dMainSpawnerCount;
        }

        [SerializeField] private List<SwitchablePartCount> m_lstAllPossibleParts;

        public List<SwitchablePartCount> AllPossibleParts
        {
            get => m_lstAllPossibleParts;
        }

        [SerializeField] private List<ScriptableEnemyWave> m_lstWaves;

        public List<ScriptableEnemyWave> AllWaves
        {
            get => m_lstWaves;
        }
    }
}
