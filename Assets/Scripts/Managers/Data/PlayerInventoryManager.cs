using BWW.Behaviours.Map;
using BWW.Behaviours.Map.Items;
using BWW.Behaviours.UI;
using BWW.Enums;
using BWW.Managers.Map;
using BWW.Managers.UI;
using BWW.Utils.UI;
using System.Collections.Generic;
using UnityEngine;

namespace BWW.Managers.Player
{
    public sealed class PlayerInventoryManager
    {
        private static PlayerInventoryManager m_instance;

        public static PlayerInventoryManager Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new PlayerInventoryManager();
                }
                return m_instance;
            }
        }

        private Dictionary<string, int> m_lstMaterialCount;

        public Dictionary<string, int> MaterialCount
        {
            get => m_lstMaterialCount;
        }

        private List<string> m_lstMaterialOrder;

        public List<string> MaterialOrder
        {
            get => m_lstMaterialOrder;
        }

        private MovableItem m_heldItem;

        public MovableItem HeldItem
        {
            get => m_heldItem;
        }

        public void PlaceHeldItem(GridCellBehaviour p_cell)
        {
            string l_sItemKey = m_heldItem.name.Replace("Curve", "");

            p_cell.PlaceItem(l_sItemKey);

            m_heldItem = null;
        }

        public void HoldItemOnCell(GridCellBehaviour p_cell)
        {
            m_heldItem = p_cell.PlacedItem;

            p_cell.TakeItem();
        }

        public void HoldItem(MovableItem p_item)
        {
            m_heldItem = p_item;

            NavMeshManager.Instance.DisableObstacle(p_item.gameObject);

            p_item.gameObject.SetActive(false);
        }

        public void AddMaterial(string p_sMaterialKey, int l_dQuantity)
        {
            bool l_bNoMaterial = !m_lstMaterialCount.ContainsKey(p_sMaterialKey) || m_lstMaterialCount[p_sMaterialKey] == 0;

            if(m_lstMaterialCount.ContainsKey(p_sMaterialKey))
            {
                m_lstMaterialCount[p_sMaterialKey] += l_dQuantity;
            }
            else if (l_dQuantity > 0)
            {
                m_lstMaterialCount.Add(p_sMaterialKey, l_dQuantity);
            }
            
            if (l_bNoMaterial)
            {
                ItemFeedbackData l_feedback = new ItemFeedbackData(EItemFeedbackType.GET_MATERIAL, p_sMaterialKey, Vector3.zero);

                ItemFeedbackManager.Instance.AddToWaitingFeedbackPool(l_feedback);

                m_lstMaterialOrder.Add(p_sMaterialKey);
            }

            GameObject.Find("ItemCounter").transform.Find(p_sMaterialKey).GetComponent<ItemCounterBehaviour>().UpdateCount(m_lstMaterialCount[p_sMaterialKey]);

            if(m_lstMaterialCount[p_sMaterialKey] == 0)
            {
                m_lstMaterialOrder.Remove(p_sMaterialKey);
            }
        }

        private PlayerInventoryManager()
        {
            m_lstMaterialCount = new Dictionary<string, int>();

            m_lstMaterialOrder = new List<string>();

            string[] l_lstMaterialKeys = new string[] { "Bricks", "Wood", "Water" };

            foreach (string l_sKey in l_lstMaterialKeys)
            {
                m_lstMaterialCount.Add(l_sKey, 0);
            }

            AddMaterial("Wood", 5);

            m_heldItem = null;
        }
    }
}
