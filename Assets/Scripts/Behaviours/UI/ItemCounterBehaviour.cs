using TMPro;
using UnityEngine;

namespace BWW.Behaviours.UI
{
   public class ItemCounterBehaviour : MonoBehaviour
   {
      private TextMeshProUGUI m_txtCountDisplay;

      public void Disappear()
      {
         gameObject.SetActive(false);
      }

      public void UpdateCount(int p_dCount)
      {
         if(m_txtCountDisplay == null)
         {
            m_txtCountDisplay = GetComponentInChildren<TextMeshProUGUI>();
         }

         m_txtCountDisplay.text = p_dCount.ToString();
      }
   }
}
