using BWW.Behaviours.Map.Items;
using BWW.Managers.Map;
using UnityEngine;
using UnityEngine.UI;

namespace BWW.Behaviours.Map
{
   public class GridCellBehaviour : MonoBehaviour
   {
      private Button m_button;

      private MovableItem m_placedItem;

      public MovableItem PlacedItem
      {
         get => m_placedItem;
         set => m_placedItem = value;
      }

      private void Start()
      {
         m_button = GetComponent<Button>();

         m_button.onClick.AddListener(() =>
         {
            GridManager.Instance.SelectedCell = this;
         });
      }

   }
}
