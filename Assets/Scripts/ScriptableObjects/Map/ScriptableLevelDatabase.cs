using System.Collections.Generic;
using UnityEngine;

namespace BWW.ScriptableObjects.Map
{
    [CreateAssetMenu(fileName = "ScriptableLevelDatabase", menuName = "BWW/ScriptableObjects/ScriptableLevelDatabase")]
    public class ScriptableLevelDatabase : ScriptableObject
    {
        [SerializeField] private List<ScriptableLevelConfiguration> m_lstLevelDatabase;

        public ScriptableLevelConfiguration GetDataById(int p_dId)
        {
            return m_lstLevelDatabase[p_dId];
        }
    }
}
