using BWW.Behaviours.Map;
using BWW.Enums;
using UnityEngine;

namespace BWW.Utils.UI
{
   public struct ItemFeedbackData
   {
      private EItemFeedbackType m_type;

      public EItemFeedbackType Type
      {
         get => m_type;
      }

      private string m_sItemKey;

      public string ItemKey
      {          
         get => m_sItemKey;
      }

      private Vector3 m_vecPosition;

      public Vector3 Position
      {
         get => m_vecPosition;
      }

      private GridCellBehaviour m_cell;

      public GridCellBehaviour Cell
      {
         get => m_cell;
      }

      public ItemFeedbackData(EItemFeedbackType p_eType, string p_sItemKey, Vector3 p_vecPosition, GridCellBehaviour p_cell  = null)
      {
         m_type = p_eType;
         m_sItemKey = p_sItemKey;
         m_vecPosition = p_vecPosition;
         m_cell = p_cell;
      }
   }
}
