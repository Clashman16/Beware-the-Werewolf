using BWW.Behaviours.Map;
using BWW.Managers.Player;

namespace BWW.Managers.Map
{
   public sealed class GridManager
   {
      private static GridManager m_instance;

      public static GridManager Instance
      {
         get
         {
            if (m_instance == null)
            {
               m_instance = new GridManager();
            }
            return m_instance;
         }
      }

      private GridCellBehaviour m_selectedCell;

      public GridCellBehaviour SelectedCell
      {
         get => m_selectedCell;
         set
         {
            m_selectedCell = value;

            if(m_selectedCell.PlacedItem == null)
            {
               if (PlayerInventoryManager.Instance.HeldItem != null)
               {
                  m_selectedCell.PlacedItem = PlayerInventoryManager.Instance.HeldItem;

                  PlayerInventoryManager.Instance.HeldItem = null;
               }
               else
               {
                  string l_sFirstMaterialKey = PlayerInventoryManager.Instance.MaterialOrder[0];

                  if (PlayerInventoryManager.Instance.MaterialCount[l_sFirstMaterialKey] > 0)
                  {
                     PlayerInventoryManager.Instance.MaterialCount[l_sFirstMaterialKey] -= 1;


                  }
               }
            }
            else
            {
               if (PlayerInventoryManager.Instance.HeldItem == null)
               {
                  PlayerInventoryManager.Instance.HeldItem = m_selectedCell.PlacedItem;

                  m_selectedCell.PlacedItem = null;
               }
            }

            m_selectedCell = null;
         }
      }

      private GridManager()
      {
         m_selectedCell = null;
      }
   }
}
