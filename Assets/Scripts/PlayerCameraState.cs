using BWW.Behaviours.UI;
using BWW.Enums;
using UnityEngine;

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

      private RotateCursorBehaviour m_rotateCursor;

      public RotateCursorBehaviour RotateCursor
      {
         get => m_rotateCursor;
      }

      public bool IsRotatingItem
      {
         get
         {
            if (m_rotateCursor == null)
            {
               m_rotateCursor = Object.FindAnyObjectByType<RotateCursorBehaviour>(FindObjectsInactive.Include);
            }

            return m_rotateCursor.gameObject.activeSelf;
         }
      }

      public abstract void UpdateState();

      public abstract bool IsClickDown();

      public abstract Vector3 GetPointerPosition();
   }
}
