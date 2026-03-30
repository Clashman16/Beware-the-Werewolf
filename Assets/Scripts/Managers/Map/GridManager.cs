using BWW.Behaviours.Map;
using BWW.Managers.UI;
using BWW.Enums;
using BWW.Managers.Player;
using BWW.Utils.UI;
using UnityEngine;

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
                  PlayerInventoryManager.Instance.PlaceHeldItem(m_selectedCell);

                  ItemFeedbackData l_feedback = new ItemFeedbackData(EItemFeedbackType.RELEASE_ITEM, "", Vector3.zero);

                  ItemFeedbackManager.Instance.AddToWaitingFeedbackPool(l_feedback);
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
                  PlayerInventoryManager.Instance.HoldItem(m_selectedCell);

                  ItemFeedbackData l_feedback = new ItemFeedbackData(EItemFeedbackType.TAKE_ITEM, PlayerInventoryManager.Instance.HeldItem.name, Vector3.zero);

                  ItemFeedbackManager.Instance.AddToWaitingFeedbackPool(l_feedback);
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
