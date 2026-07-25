using BWW.Behaviours.Map.Items;
using BWW.Managers.Map;
using UnityEngine;

namespace BWW.Utils.UI
{
    public class ItemSelectionMovable : IItemSelection
    {
        public void HandleItemSelection(Collider p_selectedItem)
        {
            MovableItem l_item = p_selectedItem.GetComponent<MovableItem>();

            if (l_item != null)
            {
                GridManager.Instance.SelectItemOnGrid(l_item);
            }
        }
    }
}
