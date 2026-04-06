using BWW.Behaviours.Map;
using BWW.Behaviours.Map.Items;
using BWW.Utils.Map;

namespace BWW.Managers.Map
{
   public sealed class ItemPlacerManager
   {
      private static ItemPlacerManager m_instance;

      public static ItemPlacerManager Instance
      {
         get
         {
            if (m_instance == null)
            {
               m_instance = new ItemPlacerManager();
            }
            return m_instance;
         }
      }

      private ItemPlacingUtility m_utility;

      private ItemPlacerManager()
      {
         m_utility = new ItemPlacingUtility();
      }

      public MovableItem PlaceItem(string p_sItemKey, GridCellBehaviour p_cell)
      {
         return m_utility.ShowItem(true, p_cell, p_sItemKey);
      }

      public MovableItem TakeItem(string p_sItemKey, GridCellBehaviour p_cell)
      {
         return m_utility.ShowItem(false, p_cell);
      }
   }
}
