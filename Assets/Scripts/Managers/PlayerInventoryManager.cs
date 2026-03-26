using BWW.Behaviours.Map.Items;
using System.Collections.Generic;

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
         set => m_heldItem = value;
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

         m_lstMaterialCount["Bricks"] = 5;

         m_lstMaterialOrder.Add(0, "Bricks");

         m_heldItem = null;
      }
   }
}
