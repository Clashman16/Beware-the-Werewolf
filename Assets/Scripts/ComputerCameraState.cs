using BWW.Enums;
using UnityEngine;

namespace BWW.Player
{
   public class ComputerCameraState : PlayerCameraState
   {
      public override void UpdateState()
      {
         float l_fmouseScrollDelta = Input.mouseScrollDelta.y;

         if (l_fmouseScrollDelta != 0f)
         {
            SimulatedControl = EControls.ZOOM;

            IsForwardZoom = l_fmouseScrollDelta > 0f;

            IsMoving = true;
         }
         else if (Input.GetKey(KeyCode.A)
            || Input.GetKey(KeyCode.Q)
            || Input.GetKey(KeyCode.LeftArrow))
         {
            SimulatedControl = EControls.CAMERA_LEFT;

            IsMoving = true;
         }
         else if(Input.GetMouseButton((int) EControls.CAMERA_RIGHT)
            || Input.GetKey(KeyCode.E)
            || Input.GetKey(KeyCode.D)
            || Input.GetKey(KeyCode.RightArrow))
         {
            SimulatedControl = EControls.CAMERA_RIGHT;

            IsMoving = true;
         }
         else
         {
            IsMoving = false;
         }
      }
   }
}
