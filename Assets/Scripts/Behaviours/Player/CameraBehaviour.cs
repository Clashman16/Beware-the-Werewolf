using BWW.Enums;
using BWW.Managers.Map;
using BWW.Player;
using BWW.Utils.UI;
using UnityEngine;

namespace BWW.Behaviours.Player
{
    public class CameraBehaviour : MonoBehaviour
    {
        [SerializeField] private string[] m_lstItemSelectionMasks;

        private Vector3 m_vecMovePivot;

        private float[] m_lstZoomLimits;

        private PlayerCameraState m_state;

        [SerializeField] private float m_fSpeed = 50f;

        private Camera m_camera;

        public PlayerCameraState State
        {
            get => m_state;
            set
            {
                m_state = value;

                m_state.IsMoving = false;
            }
        }

        private void Start()
        {
            m_vecMovePivot = new Vector3(18.0540009f, 0, -34.9550018f);

            m_lstZoomLimits = new[] { 4, 8.5f };

            m_camera = GetComponent<Camera>();
        }

        private void Update()
        {
            if (GridManager.Instance.SelectedCell == null)
            {
                m_state.UpdateState();

            if(m_state.IsClickDown() && !m_state.IsRotatingItem)
                {
                    Ray l_ray = m_camera.ScreenPointToRay(m_state.GetPointerPosition());

                    foreach(string l_sMask in m_lstItemSelectionMasks)
                    {
                        if (Physics.Raycast(l_ray, out RaycastHit l_hit, 100f, LayerMask.GetMask(l_sMask)))
                        {
                            if(l_sMask == "MovableItem")
                            {
                                new ItemSelectionMovable().HandleItemSelection(l_hit.collider);
                            }
                            else
                            {
                                new ItemSelectionResource().HandleItemSelection(l_hit.collider);
                            }
                        }
                    }
                }
            }

         if(m_state.IsMoving)
            {
            if(m_state.SimulatedControl == EControls.ZOOM)
                {
                    float l_fCurrentPosition = transform.position.y;

                    if ((m_state.IsForwardZoom && l_fCurrentPosition > m_lstZoomLimits[0])
                       || (!m_state.IsForwardZoom && l_fCurrentPosition < m_lstZoomLimits[1]))
                    {
                        Vector3 l_vecZoomDirection = transform.forward * (m_state.IsForwardZoom ? 1 : -1);

                        transform.Translate(l_vecZoomDirection * Time.deltaTime * m_fSpeed/2, Space.World);
                    }
                }
                else
                {
                    float l_fAngle = m_state.SimulatedControl == EControls.CAMERA_LEFT ? 1 : -1;

                    transform.RotateAround(m_vecMovePivot, Vector3.up, l_fAngle * Time.deltaTime * m_fSpeed);
                }
            }
        }

        public void SetIsMoving(bool p_bIsMoving)
        {
            m_state.IsMoving = p_bIsMoving;
        }
    }
}
