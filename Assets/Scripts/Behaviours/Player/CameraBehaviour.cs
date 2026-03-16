using BWW.Player;
using UnityEngine;

namespace BWW.Behaviours.Player
{
   public class CameraBehaviour : MonoBehaviour
   {

      private Vector3 m_vecMovePivot;

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
      }

      private void Update()
      {
         m_state.UpdateState();

         if(m_state.IsMoving)
         {
            float l_fAngle = m_state.SimulatedButton == Enums.EMouseButton.LEFT ? 1 : -1;

            transform.RotateAround(m_vecMovePivot, Vector3.up, l_fAngle);
         }
      }
   }
}
