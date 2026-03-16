using BWW.Enums;
using UnityEngine;

namespace BWW.Player
{
   public class ComputerCameraState : PlayerCameraState
   {
      public override void UpdateState()
      {
         if (Input.GetMouseButton((int) EMouseButton.LEFT))
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
