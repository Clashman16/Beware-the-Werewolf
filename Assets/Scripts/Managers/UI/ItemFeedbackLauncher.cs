using BWW.Behaviours.UI;
using BWW.Utils.UI;
using System.Collections.Generic;

namespace BWW.Managers.UI
{
   public abstract class ItemFeedbackLauncher
   {
      private Queue<ItemFeedbackBehaviour> m_lstItemFeedbackObjectPool;

      internal Queue<ItemFeedbackBehaviour> ItemFeedbackObjectPool
      {
         get => m_lstItemFeedbackObjectPool;
         set => m_lstItemFeedbackObjectPool = value;
      }

      public abstract void HandleFeedback(ItemFeedbackData p_data);
   }
}
