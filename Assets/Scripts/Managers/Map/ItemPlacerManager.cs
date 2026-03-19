using BWW.Behaviours.Map;
using BWW.Behaviours.Map.Items;

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

      public MovableItem PlaceItem(string p_sItemKey, GridCellBehaviour p_cell)
      {
         return new MovableItem();
      }
   }
}
