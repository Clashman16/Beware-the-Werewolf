using BWW.Behaviours.Map;
using BWW.Behaviours.Map.Items;
using BWW.Enums;
using BWW.Managers.Player;
using BWW.Managers.UI;
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

      private bool m_bIgnoreNextClick;

      private GridCellBehaviour m_selectedCell;

      public GridCellBehaviour SelectedCell
      {
         get => m_selectedCell;
         set
         {
            if(m_bIgnoreNextClick)
            {
               m_bIgnoreNextClick = false;

               return;
            }

            m_selectedCell = value;

            if(m_selectedCell.PlacedItem == null)
            {
               if (PlayerInventoryManager.Instance.HeldItem != null)
               {
                  PlaceHeldItem();
               }
               else
               {
                  string l_sFirstMaterialKey = PlayerInventoryManager.Instance.MaterialOrder[0];

                  if (PlayerInventoryManager.Instance.MaterialCount[l_sFirstMaterialKey] > 0)
                  {
                     PlayerInventoryManager.Instance.AddMaterial(l_sFirstMaterialKey, -1);

                     ItemFeedbackData l_feedback = new ItemFeedbackData(EItemFeedbackType.PLACE_ITEM, l_sFirstMaterialKey, m_selectedCell.transform.position, m_selectedCell);

                     ItemFeedbackManager.Instance.AddToWaitingFeedbackPool(l_feedback);
                  }
               }
            }
            else
            {
               if(PlayerInventoryManager.Instance.HeldItem == null)
               {
                  PlayerInventoryManager.Instance.HoldItemOnCell(m_selectedCell);

                  ItemFeedbackData l_feedback = new ItemFeedbackData(EItemFeedbackType.TAKE_ITEM, PlayerInventoryManager.Instance.HeldItem.name, Vector3.zero);

                  ItemFeedbackManager.Instance.AddToWaitingFeedbackPool(l_feedback);
               }
               else
               {
                  HayRollBehaviour[] l_lstPlayerlHayRoll = PlayerInventoryManager.Instance.HeldItem.GetComponentsInChildren<HayRollBehaviour>();
                  HayRollBehaviour[] l_lstCellHayRoll = m_selectedCell.PlacedItem.GetComponentsInChildren<HayRollBehaviour>();

                  if (l_lstCellHayRoll.Length > 0 && l_lstPlayerlHayRoll.Length + l_lstCellHayRoll.Length <= 3)
                  {
                     PlaceHeldItem();
                  }
               }
            }

            m_selectedCell = null;
         }
      }

      private void PlaceHeldItem(GridCellBehaviour p_cell = null)
      {
         PlayerInventoryManager.Instance.PlaceHeldItem(p_cell == null ? m_selectedCell : p_cell);

         ItemFeedbackData l_feedback = new ItemFeedbackData(EItemFeedbackType.RELEASE_ITEM, "", Vector3.zero);

         ItemFeedbackManager.Instance.AddToWaitingFeedbackPool(l_feedback);
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

      public void SelectItemOnGrid(MovableItem p_selectedItem)
      {
         m_bIgnoreNextClick = true;

         if (PlayerInventoryManager.Instance.HeldItem == null)
         {
            GridCellBehaviour l_cell = p_selectedItem.transform.parent.GetComponent<GridCellBehaviour>();

            if(l_cell == null)
            {
               PlayerInventoryManager.Instance.HoldItem(p_selectedItem);
            }
            else
            {
               PlayerInventoryManager.Instance.HoldItemOnCell(l_cell);

               ItemFeedbackData l_feedback = new ItemFeedbackData(EItemFeedbackType.TAKE_ITEM, PlayerInventoryManager.Instance.HeldItem.name, Vector3.zero);

               ItemFeedbackManager.Instance.AddToWaitingFeedbackPool(l_feedback);
            }
         }
         else
         {
            HayRollBehaviour[] l_lstPlayerlHayRoll = PlayerInventoryManager.Instance.HeldItem.GetComponentsInChildren<HayRollBehaviour>();

            GridCellBehaviour l_cell = p_selectedItem.GetComponentInParent<GridCellBehaviour>();

            HayRollBehaviour[] l_lstCellHayRoll = l_cell.GetComponentsInChildren<HayRollBehaviour>();

            if (l_lstCellHayRoll.Length > 0 && l_lstPlayerlHayRoll.Length + l_lstCellHayRoll.Length <= 3)
            {
               PlaceHeldItem(l_cell);
            }
         }
      }
   }
}
