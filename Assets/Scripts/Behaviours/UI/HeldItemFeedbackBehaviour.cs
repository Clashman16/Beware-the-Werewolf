using BWW.Behaviours.Map.Items;
using BWW.Managers.Player;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BWW.Behaviours.UI
{
   public class HeldItemFeedbackBehaviour : MonoBehaviour
   {
      [SerializeField]
      private List<Sprite> m_lstAllPossibleIcons;

      private Image m_imgIcon;

      public void Init(string p_sItemKey)
      {
         string l_sNewKey = p_sItemKey.Replace("Curve", "");

         if(l_sNewKey.Contains("HayRoll"))
         {
            HayRollBehaviour[] l_lstHayRolls = PlayerInventoryManager.Instance.HeldItem.GetComponentsInChildren<HayRollBehaviour>();

            l_sNewKey = l_sNewKey.Split(' ')[0];

            l_sNewKey = string.Concat(l_sNewKey, "_", l_lstHayRolls.Length.ToString());
         }

         int l_spriteIndex;

         switch(l_sNewKey)
         {
            case "HayRoll_1":

               l_spriteIndex = 0;

               break;

            case "HayRoll_2":

               l_spriteIndex = 1;

               break;

            case "HayRoll_3":

               l_spriteIndex = 2;

               break;

            case "Wood":

               l_spriteIndex = 3;

               break;

            default:

               l_spriteIndex = 4;

               break;
         }

         if(m_imgIcon == null)
         {
            m_imgIcon = GetComponentInChildren<Image>();
         }

         m_imgIcon.sprite = m_lstAllPossibleIcons[l_spriteIndex];
      }

      private void Update()
      {
         transform.position = Input.mousePosition;
      }
   }
}
