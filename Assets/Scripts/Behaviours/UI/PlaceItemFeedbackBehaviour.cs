using BWW.Behaviours.Map;
using BWW.Managers.Player;
using BWW.Utils.Items;
using BWW.Utils.UI;
using UnityEngine;
using UnityEngine.UI;

namespace BWW.Behaviours.UI
{
    public class PlaceItemFeedbackBehaviour : ItemFeedbackBehaviour
    {
        [SerializeField] private float m_fSpeed = 1200f;

        private Image m_imgIcon;

        private Vector3 m_vecAnimEndPosition;

        private Camera m_camera;

        private GridCellBehaviour m_cell;

        public GridCellBehaviour Cell
        {
            get => m_cell;
        }

        public override void Init(ItemFeedbackData p_data)
        {
            if (m_imgIcon == null)
            {
                m_imgIcon = transform.GetChild(0).GetComponent<Image>();
            }

            Transform l_counterTrf = GetCounterTransform(p_data.ItemKey);


            if(p_data.Type == Enums.EItemFeedbackType.PLACE_ITEM)
            {
                m_imgIcon.sprite = l_counterTrf.GetChild(0).GetComponent<Image>().sprite;

                transform.position = l_counterTrf.position;

                m_vecAnimEndPosition = GetScreenPosition(p_data.Position);

                m_cell = p_data.Cell;
            }
            else
            {
                m_imgIcon.sprite = InventoryItemGetter.Instance.GetItemFromKey(p_data.ItemKey).Icon;

                transform.position = GetScreenPosition(p_data.Position);

                m_vecAnimEndPosition = l_counterTrf.position;

                m_cell = null;
            }

            base.Init(p_data);
        }

        private Transform GetCounterTransform(string p_sItemKey)
        {
            return GameObject.Find("ItemCounter").transform.Find(p_sItemKey);
        }

        private Vector2 GetScreenPosition(Vector3 p_vecPositionToConvert)
        {
            if(m_camera == null)
            {
                m_camera = PlayerCameraManager.Instance.BWWCamera.UnityCamera;
            }

            return RectTransformUtility.WorldToScreenPoint(m_camera, p_vecPositionToConvert);
        }

        public override void Animate()
        {
            if(m_cell != null)
            {
                m_vecAnimEndPosition = GetScreenPosition(m_cell.transform.position);
            }
            else
            {
                m_vecAnimEndPosition = GetCounterTransform(ItemKey).position;
            }

            transform.position = Vector3.MoveTowards(transform.position, m_vecAnimEndPosition, m_fSpeed * Time.deltaTime);
        }

        public override bool IsAnimationEnded()
        {
            return Vector3.Distance(transform.position, m_vecAnimEndPosition) <= 0.001f;
        }
    }
}
