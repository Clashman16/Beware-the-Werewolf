using BWW.Utils.UI;
using UnityEngine;
using UnityEngine.Events;

namespace BWW.Behaviours.UI
{
   public abstract class ItemFeedbackBehaviour : MonoBehaviour
   {
      private bool m_bIsAnimationRunning;

      public bool IsAnimationRunning
      {
         get => m_bIsAnimationRunning;
      }

      private UnityAction m_onAnimationEnd;

      public UnityAction OnAnimationEnd
      {
         set => m_onAnimationEnd = value;
      }

      private string m_sItemKey;

      public string ItemKey
      {
         get => m_sItemKey;
      }

      public virtual void Init(ItemFeedbackData p_data)
      {
         m_sItemKey = p_data.ItemKey;

         m_bIsAnimationRunning = true;
      }

      public abstract void Animate();

      public abstract bool IsAnimationEnded();

      private void Update()
      {
         if(m_bIsAnimationRunning)
         {
            Animate();
         }

         if(IsAnimationEnded())
         {
            m_onAnimationEnd.Invoke();
            m_bIsAnimationRunning = false;
         }
      }
   }
}
