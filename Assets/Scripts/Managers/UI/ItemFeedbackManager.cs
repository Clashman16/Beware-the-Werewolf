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

      private Queue<ItemFeedbackData> m_lstWaitingFeedbackPool;

      private PlaceItemFeedbackLauncher m_placeItemFeedbackLauncher;

      private ItemFeedbackManager()
      {
         m_lstWaitingFeedbackPool = new Queue<ItemFeedbackData>();

         m_placeItemFeedbackLauncher = new PlaceItemFeedbackLauncher();
      }

      public void AddToWaitingFeedbackPool(ItemFeedbackData p_data)
      {
         m_lstWaitingFeedbackPool.Enqueue(p_data);

         switch(p_data.Type)
         {
            case EItemFeedbackType.PLACE_ITEM:

               m_placeItemFeedbackLauncher.HandleFeedback(p_data);

               break;
            case EItemFeedbackType.GET_MATERIAL:
               break;
            default:
               break;
         }
      }
   }
}
