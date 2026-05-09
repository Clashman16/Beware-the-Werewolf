using UnityEngine;

namespace BWW.Behaviours.UI
{
   public class RotateCursorBehaviour : MonoBehaviour
   {
      public void Init()
      {

      }

      public void Rotate(float p_fAngle)
      {
         transform.RotateAround(transform.position, Vector3.up, p_fAngle);
      }
   }
}
