using UnityEngine;

namespace BWW.Behaviours.UI
{
   public class RotateCursorBehaviour : MonoBehaviour
   {
      public void Init(Vector3 p_vecPosition, float p_fAngle)
      {
         transform.position = p_vecPosition;

         Rotate(p_fAngle);
      }

      public void Rotate(float p_fAngle)
      {
         transform.RotateAround(transform.position, Vector3.up, p_fAngle);
      }
   }
}
