using BWW.Behaviours.Map;
using BWW.Enums;
using BWW.Managers.Map;
using BWW.Managers.Player;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BWW.Behaviours.UI
{
    public class GridSelectionBehaviour : MonoBehaviour
    {
        private Camera m_camera;

        private GridCellBehaviour m_hoveredCell;

        [SerializeField] private LayerMask m_gridLayer;

        [SerializeField] private float m_fCellSpacing = 0.3f;

        [SerializeField] private Vector2 m_vecFirstCellLocalPosition = new Vector2(-1f, -1f);

        private void Start()
        {
            m_camera = Camera.main;
        }

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            Ray l_ray = m_camera.ScreenPointToRay(PlayerCameraManager.Instance.BWWCamera.State.GetPointerPosition());

            GridCellBehaviour l_currentCell = null;

            if (Physics.Raycast(l_ray, out RaycastHit l_hit, 100f, m_gridLayer))
            {
                l_currentCell = GetCellFromHitPoint(l_hit.point);
            }

            if (l_currentCell != m_hoveredCell)
            {
                if (m_hoveredCell != null
                    && m_hoveredCell.State != EGridCellState.DISABLED)
                {
                    UpdateHoveredCellState(EGridCellState.NORMAL);
                }

                m_hoveredCell = l_currentCell;

                if (m_hoveredCell != null
                    && m_hoveredCell.State != EGridCellState.DISABLED)
                {
                    UpdateHoveredCellState(EGridCellState.HOVERED);
                }
            }

            if (PlayerCameraManager.Instance.BWWCamera.State.IsClickDown()
                && m_hoveredCell != null
                && m_hoveredCell.State != EGridCellState.DISABLED)
            {
                GridManager.Instance.SelectedCell = m_hoveredCell;

                UpdateHoveredCellState(EGridCellState.SELECTED);
            }
        }

        private GridCellBehaviour GetCellFromHitPoint(Vector3 p_vecHitPoint)
        {
            Vector3 l_vecLocal = transform.InverseTransformPoint(p_vecHitPoint);

            int l_dX = Mathf.RoundToInt((l_vecLocal.x - m_vecFirstCellLocalPosition.x) / m_fCellSpacing);
            int l_dY = Mathf.RoundToInt((l_vecLocal.y - m_vecFirstCellLocalPosition.y) / m_fCellSpacing);

            int l_dGridSize = GridManager.Instance.GridSize;

            if (l_dX < 0 || l_dX >= l_dGridSize || l_dY < 0 || l_dY >= l_dGridSize)
                return null;

            int l_dCellIndex = l_dY * l_dGridSize + l_dX;

            if(l_dCellIndex != -1)
            {
                return transform.Find($"GridCell ({l_dCellIndex})").GetComponent<GridCellBehaviour>();
            }

            return null;
        }

        private void UpdateHoveredCellState(EGridCellState p_eState)
        {
            m_hoveredCell.State = p_eState;

            m_hoveredCell.GetComponent<GridCellAppearanceBehaviour>().UpdateAppearance(m_hoveredCell.State);
        }
    }
}
