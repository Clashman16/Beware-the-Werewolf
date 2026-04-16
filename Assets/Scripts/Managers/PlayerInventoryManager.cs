using BWW.Behaviours.Map;
using BWW.Behaviours.Map.Items;
using BWW.Behaviours.UI;
using BWW.Enums;
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

      private Dictionary<int, string> m_lstMaterialOrder;

      public Dictionary<int, string> MaterialOrder
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

         p_item.gameObject.SetActive(false);
      }

      public void AddMaterial(string p_sMaterialKey, int l_dQuantity)
      {
         bool l_bNoMaterial = m_lstMaterialCount[p_sMaterialKey] == 0;

         m_lstMaterialCount[p_sMaterialKey] += l_dQuantity;

         if (l_bNoMaterial)
         {
            ItemFeedbackData l_feedback = new ItemFeedbackData(EItemFeedbackType.GET_MATERIAL, p_sMaterialKey, Vector3.zero);

            ItemFeedbackManager.Instance.AddToWaitingFeedbackPool(l_feedback);
         }

         GameObject.Find("ItemCounter").transform.Find(p_sMaterialKey).GetComponent<ItemCounterBehaviour>().UpdateCount(m_lstMaterialCount[p_sMaterialKey]);
      }

      private PlayerInventoryManager()
      {
         m_lstMaterialCount = new Dictionary<string, int>();

         m_lstMaterialOrder = new Dictionary<int, string>();

         string[] l_lstMaterialKeys = new string[] { "Bricks", "Wood", "Water" };

         foreach (string l_sKey in l_lstMaterialKeys)
         {
            m_lstMaterialCount.Add(l_sKey, 0);
         }

         AddMaterial("Bricks", 5);

         m_lstMaterialOrder.Add(0, "Bricks");

         m_heldItem = null;
      }
   }
}
