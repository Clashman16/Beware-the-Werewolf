using UnityEngine;

namespace BWW.Behaviours.UI
{
   public class RotateCursorBehaviour : MonoBehaviour
   {
      private Transform m_targetTrf;

        private RectTransform m_trf;

        private Camera m_camera;

      public void Init(Transform p_targetTrf, float p_fAngle)
      {
            if (m_trf == null)
            {
                m_trf = GetComponent<RectTransform>();
            }

         m_targetTrf = p_targetTrf;

            UpdateScreenPosition();

            m_trf.localRotation = Quaternion.identity;

         Rotate(p_fAngle);
      }

      public void Rotate(float p_fAngle)
      {
         transform.RotateAround(transform.position, Vector3.up, p_fAngle);
      }

      public void Check()
      {
         m_targetTrf = null;

         gameObject.SetActive(false);
      }

      public void RotateTarget(bool p_bClockwise)
      {
         int l_dAngle = 90;

         if(!p_bClockwise)
         {
            l_dAngle *= -1;
         }

         m_targetTrf.RotateAround(m_targetTrf.position, Vector3.up, l_dAngle);
      }

        private void LateUpdate()
        {
            if (m_targetTrf == null)
                return;

            UpdateScreenPosition();
        }

        private void UpdateScreenPosition()
        {
            if(m_camera == null)
            {
                m_camera = Camera.main;
            }

            Vector3 l_vecScreenPos = m_camera.WorldToScreenPoint(m_targetTrf.position);

            m_trf.position = l_vecScreenPos;
        }
   }
}
