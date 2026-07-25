using BWW.Behaviours.Map.Items;
using BWW.Enums;
using BWW.Managers.UI;
using BWW.Utils.UI;
using UnityEngine;

public class ItemSelectionResource : IItemSelection
{
    public void HandleItemSelection(Collider p_selectedItem)
    {
        ResourceItem l_item = p_selectedItem.GetComponent<ResourceItem>();

        if (l_item != null)
        {
            ItemFeedbackData l_feedback = new ItemFeedbackData(EItemFeedbackType.TAKE_MATERIAL, l_item.ID, l_item.transform.position);

            ItemFeedbackManager.Instance.AddToWaitingFeedbackPool(l_feedback);
        }
    }
}
