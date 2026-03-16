using BWW.Enums;
using UnityEngine;

namespace BWW.Player
{
   public class ComputerCameraState : PlayerCameraState
   {
      public override void UpdateState()
      {
         float l_fmouseScrollDelta = Input.mouseScrollDelta.y;

         if (l_fmouseScrollDelta < 0f || l_fmouseScrollDelta > 0f)
         {
            SimulatedButton = EMouseButton.SCROLL;

            IsForwardZoom = l_fmouseScrollDelta > 0f;

            IsMoving = true;
         }
         else if (Input.GetMouseButton((int) EMouseButton.LEFT))
         {
            SimulatedButton = EMouseButton.LEFT;

            IsMoving = true;
         }
         else if(Input.GetMouseButton((int) EMouseButton.RIGHT))
         {
            SimulatedButton = EMouseButton.RIGHT;

            IsMoving = true;
         }
         else
         {
            IsMoving = false;
         }
      }
   }
}
