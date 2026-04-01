using BWW.Utils.UI;
using UnityEngine;

namespace BWW.Managers.UI
{
   public class MaterialFeedbackLauncher : ItemFeedbackLauncher
   {
      public override void HandleFeedback(ItemFeedbackData p_data)
      {
         GameObject l_goCounterSlot = GameObject.Find("ItemCounter").transform.Find(p_data.ItemKey).gameObject;

         l_goCounterSlot.SetActive(true);

         Animator l_animator = l_goCounterSlot.GetComponent<Animator>();

         l_animator.SetTrigger(p_data.Type.ToString());
      }
   }
}
