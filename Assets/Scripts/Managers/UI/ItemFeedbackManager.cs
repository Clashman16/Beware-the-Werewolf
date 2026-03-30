using BWW.Enums;
using BWW.Utils.UI;
using System.Collections.Generic;

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

      private ItemFeedbackManager()
      {
         m_placeItemFeedbackLauncher = new PlaceItemFeedbackLauncher();

         m_materialFeedbackLauncher = new MaterialFeedbackLauncher();
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

            default:

               break;
         }
      }
   }
}
