using BWW.Behaviours.UI;
using BWW.Utils.UI;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace BWW.Managers.UI
{
   public class PlaceItemFeedbackLauncher : ItemFeedbackLauncher
   {
      public PlaceItemFeedbackLauncher()
      {
         ItemFeedbackObjectPool = new Queue<ItemFeedbackBehaviour>();

         PlaceItemFeedbackBehaviour[] l_lstPlaceItemFeedback = Object.FindObjectsByType<PlaceItemFeedbackBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None);

         foreach (PlaceItemFeedbackBehaviour l_feedbackObject in l_lstPlaceItemFeedback)
         {
            l_feedbackObject.gameObject.SetActive(true);

            l_feedbackObject.OnAnimationEnd = () =>
            {
               ItemFeedbackObjectPool.Enqueue(l_feedbackObject);

               l_feedbackObject.Cell.PlaceItem(l_feedbackObject.ItemKey);

               l_feedbackObject.gameObject.SetActive(false);
            };

            l_feedbackObject.gameObject.SetActive(false);

            ItemFeedbackObjectPool.Enqueue(l_feedbackObject);
         }
      }

      public override async void HandleFeedback(ItemFeedbackData p_data)
      {
         if (ItemFeedbackObjectPool.Count > 0)
         {
            PlaceItemFeedbackBehaviour l_feedbackObject = (PlaceItemFeedbackBehaviour) ItemFeedbackObjectPool.Dequeue();

            l_feedbackObject.gameObject.SetActive(true);

            l_feedbackObject.Init(p_data);
         }
         else
         {
            PlaceItemFeedbackBehaviour[] l_lstPlaceItemFeedback = Object.FindObjectsByType<PlaceItemFeedbackBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            if (l_lstPlaceItemFeedback.Length < 8)
            {
               PlaceItemFeedbackBehaviour l_feedbackObjectTemplate = l_lstPlaceItemFeedback[0];

               GameObject l_feedbackObjectCopy = Object.Instantiate(l_feedbackObjectTemplate.gameObject, l_feedbackObjectTemplate.transform.parent);

               PlaceItemFeedbackBehaviour l_feedbackObject = l_feedbackObjectCopy.GetComponent<PlaceItemFeedbackBehaviour>();

               l_feedbackObject.Init(p_data);
            }
            else
            {
               await WaitForFeedbackCatcher(p_data);
            }
         }
      }

      private async Task WaitForFeedbackCatcher(ItemFeedbackData p_data)
      {
         while (ItemFeedbackObjectPool.Count == 0)
         {
            await Task.Delay(50);
         }

         HandleFeedback(p_data);
      }
   }
}
