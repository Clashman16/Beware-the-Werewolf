using System.Collections.Generic;
using UnityEngine;

namespace BWW.ScriptableObjects.Items
{
    [CreateAssetMenu(fileName = "ScriptableInventoryItemDatabase", menuName = "BWW/ScriptableObjects/ScriptableInventoryItemDatabase")]
    public class ScriptableInventoryItemDatabase : ScriptableObject
    {
        [SerializeField] private List<ScriptableInventoryItem> m_lstInventoryItemDatabase;

        [SerializeField] private List<string> m_lstItemKeys;

        public ScriptableInventoryItem GetItemFromKey(string p_sKey)
        {
            return m_lstInventoryItemDatabase[m_lstItemKeys.IndexOf(p_sKey)];
        }
    }
}
