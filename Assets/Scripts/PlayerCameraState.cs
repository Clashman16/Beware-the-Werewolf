using BWW.Enums;

namespace BWW.Player
{
   public abstract class PlayerCameraState
   {
      private bool m_bIsMoving;

      public bool IsMoving
      {
         get => m_bIsMoving;
         set => m_bIsMoving = value;
      }

      private EMouseButton m_eSimulatedButton;

      public EMouseButton SimulatedButton
      {
         get => m_eSimulatedButton;
         set => m_eSimulatedButton = value;
      }

      public abstract void UpdateState();
   }
}
