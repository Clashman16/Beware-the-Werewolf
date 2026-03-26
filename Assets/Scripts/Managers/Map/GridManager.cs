using BWW.Behaviours.Map;
using BWW.Managers.UI;
using BWW.Enums;
using BWW.Managers.Player;
using BWW.Utils.UI;

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

                     ItemFeedbackData l_feedback = new ItemFeedbackData(EItemFeedbackType.PLACE_ITEM, l_sFirstMaterialKey, m_selectedCell.transform.position, m_selectedCell);

                     ItemFeedbackManager.Instance.AddToWaitingFeedbackPool(l_feedback);
                  }
               }
            }
            else
            {
               if (PlayerInventoryManager.Instance.HeldItem == null)
               {
                  PlayerInventoryManager.Instance.HeldItem = m_selectedCell.PlacedItem;

                  m_selectedCell.TakeItem();

                  m_selectedCell.PlacedItem = null;
               }
            }

            m_selectedCell = null;
         }
      }

      private const int m_dGridSize = 8;

      public int GridSize
      {
         get => m_dGridSize;
      }

      private GridManager()
      {
         m_selectedCell = null;
      }
   }
}
