using BWW.ScriptableObjects.Items;
using UnityEngine;

namespace BWW.Utils.Items
{
    public sealed class InventoryItemGetter
    {
        private const string m_sDatabasePath = "ScriptableObjects/Items/InventoryItems";

        private static InventoryItemGetter m_instance;

        public static InventoryItemGetter Instance
        {
            get
            {
                if(m_instance == null)
                {
                    m_instance = new InventoryItemGetter();
                }

                return m_instance;
            }
        }

        private ScriptableInventoryItemDatabase m_database;

        private InventoryItemGetter()
        {
            m_database = Resources.Load<ScriptableInventoryItemDatabase>(m_sDatabasePath);
        }

        public ScriptableInventoryItem GetItemFromKey(string p_sKey)
        {
            return m_database.GetItemFromKey(p_sKey);
        }
    }
}
