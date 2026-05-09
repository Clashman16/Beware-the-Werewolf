using UnityEngine;

namespace BWW.Behaviours.UI
{
   public class RotateCursorBehaviour : MonoBehaviour
   {
      private Transform m_targetTrf;

      public void Init(Transform p_targetTrf, float p_fAngle)
      {
         m_targetTrf = p_targetTrf;

         transform.position = m_targetTrf.position;

         transform.localRotation = Quaternion.identity;

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
   }
}
