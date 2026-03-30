using BWW.Behaviours.UI;
using BWW.Enums;
using BWW.Utils.UI;
using System.Collections.Generic;
using UnityEngine;

namespace BWW.Managers.UI
{
   public sealed class ItemFeedbackManager
   {
      private static ItemFeedbackManager m_instance;

      public static ItemFeedbackManager Instance
      {
         get
         {
            if (m_instance == null)
            {
               m_instance = new ItemFeedbackManager();
            }
            return m_instance;
         }
      }

      private PlaceItemFeedbackLauncher m_placeItemFeedbackLauncher;

      private MaterialFeedbackLauncher m_materialFeedbackLauncher;

      private HeldItemFeedbackBehaviour m_heldItemFeedback;

      private ItemFeedbackManager()
      {
         m_placeItemFeedbackLauncher = new PlaceItemFeedbackLauncher();

         m_materialFeedbackLauncher = new MaterialFeedbackLauncher();

         m_heldItemFeedback = Object.FindAnyObjectByType<HeldItemFeedbackBehaviour>(FindObjectsInactive.Include);
      }

      public void AddToWaitingFeedbackPool(ItemFeedbackData p_data)
      {
         switch(p_data.Type)
         {
            case EItemFeedbackType.PLACE_ITEM:

               m_placeItemFeedbackLauncher.HandleFeedback(p_data);

               break;

            case EItemFeedbackType.GET_MATERIAL:
            case EItemFeedbackType.LOOSE_MATERIAL:

               m_materialFeedbackLauncher.HandleFeedback(p_data);

               break;

            case EItemFeedbackType.TAKE_ITEM:

               m_heldItemFeedback.gameObject.SetActive(true);

               m_heldItemFeedback.Init(p_data.ItemKey);

               break;

            case EItemFeedbackType.RELEASE_ITEM:

               m_heldItemFeedback.gameObject.SetActive(false);

               break;

            default:

               break;
         }
      }
   }
}
