using BWW.Behaviours.Map;
using BWW.Utils.UI;
using UnityEngine;
using UnityEngine.UI;

namespace BWW.Behaviours.UI
{
   public class PlaceItemFeedbackBehaviour : ItemFeedbackBehaviour
   {
      [SerializeField]
      private float m_fSpeed = 1200f;

      private Image m_imgIcon;

      private Vector3 m_vecAnimEndPosition;

      private GridCellBehaviour m_cell;

      public GridCellBehaviour Cell
      {
         get => m_cell;
      }

      public override void Init(ItemFeedbackData p_data)
      {
         m_vecAnimEndPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, p_data.Position);

         if(m_imgIcon == null)
         {
            m_imgIcon = transform.GetChild(0).GetComponent<Image>();
         }

         Transform l_counterTrf = GameObject.Find("ItemCounter").transform.Find(p_data.ItemKey);

         m_imgIcon.sprite = l_counterTrf.GetChild(0).GetComponent<Image>().sprite;

         transform.position = l_counterTrf.position;

         m_cell = p_data.Cell;

         base.Init(p_data);
      }

      public override void Animate()
      {
         m_vecAnimEndPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, m_cell.transform.position);

         transform.position = Vector3.MoveTowards(transform.position, m_vecAnimEndPosition, m_fSpeed * Time.deltaTime);
      }

      public override bool IsAnimationEnded()
      {
         return Vector3.Distance(transform.position,m_vecAnimEndPosition) <= 0.001f;
      }
   }
}
