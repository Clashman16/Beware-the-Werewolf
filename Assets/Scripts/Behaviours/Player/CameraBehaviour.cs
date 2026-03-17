using BWW.Enums;
using BWW.Player;
using UnityEngine;

namespace BWW.Behaviours.Player
{
   public class CameraBehaviour : MonoBehaviour
   {
      private Vector3 m_vecMovePivot;

      private int[] m_lstZoomLimits;

      PlayerCameraState m_state;

      public PlayerCameraState State
      {
         set
         {
            m_state = value;

            m_state.IsMoving = false;
         }
      }

      private void Start()
      {
         m_vecMovePivot = new Vector3(18.0540009f, 0, -34.9550018f);

         m_lstZoomLimits = new[]{ 3, 7 };
      }

      private void Update()
      {
         if(GridManager.Instance.SelectedCell == null)
         {
         m_state.UpdateState();
         }

         if(m_state.IsMoving)
         {
            if(m_state.SimulatedButton == EMouseButton.SCROLL)
            {
               float l_fCurrentPosition = transform.position.y;

               if ((m_state.IsForwardZoom && l_fCurrentPosition > m_lstZoomLimits[0])
                  || (!m_state.IsForwardZoom && l_fCurrentPosition < m_lstZoomLimits[1]))
               {
                  Vector3 l_vecZoomDirection = transform.forward * (m_state.IsForwardZoom ? 1 : -1);

                  transform.Translate(l_vecZoomDirection, Space.World);
               }
            }
            else
            {
               float l_fAngle = m_state.SimulatedButton == EMouseButton.LEFT ? 1 : -1;

               transform.RotateAround(m_vecMovePivot, Vector3.up, l_fAngle);
            }
         }
      }
   }
}
