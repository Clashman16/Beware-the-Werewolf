using System.Collections.Generic;
using UnityEngine;

namespace BWW.ScriptableObjects.Map
{
    [CreateAssetMenu(fileName = "ScriptableEnemyWave", menuName = "BWW/ScriptableObjects/ScriptableEnemyWave")]
    public class ScriptableEnemyWave : ScriptableObject
    {
        [SerializeField] private List<EnemyCount> m_lstAllPossibleEnemies;
    }
}
