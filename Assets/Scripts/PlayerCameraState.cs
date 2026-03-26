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

      private bool m_bIsForwardZoom;

      public bool IsForwardZoom
      {
         get => m_bIsForwardZoom;
         set => m_bIsForwardZoom = value;
      }

      private EControls m_eSimulatedControl;

      public EControls SimulatedControl
      {
         get => m_eSimulatedControl;
         set => m_eSimulatedControl = value;
      }

      public abstract void UpdateState();
   }
}
