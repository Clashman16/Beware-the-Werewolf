using BWW.Enums;
using System;
using UnityEngine;

namespace BWW.Behaviours.UI
{
    public class GridCellAppearanceBehaviour : MonoBehaviour
    {
        private SpriteRenderer m_appearance;

        [SerializeField] private Sprite[] m_lstSpriteSheet;

        [SerializeField] private Color[] m_lstColor;

        private void Start()
        {
            m_appearance = GetComponent<SpriteRenderer>();

            Init();
        }

        private void Init()
        {
            UpdateAppearance(EGridCellState.NORMAL);
        }

        public void UpdateAppearance(EGridCellState p_eState)
        {
            int l_dSpriteIndex;

            int l_dColorIndex;

            switch(p_eState)
            {
                case EGridCellState.SELECTED:

                    l_dSpriteIndex = 0;

                    l_dColorIndex = 1;

                    break;
                case EGridCellState.HOVERED:

                    l_dSpriteIndex = 1;

                    l_dColorIndex = 1;

                    break;
                case EGridCellState.DISABLED:

                    l_dSpriteIndex = 2;

                    l_dColorIndex = 2;

                    break;
                default:

                    l_dSpriteIndex = 0;

                    l_dColorIndex = 0;

                    break;
            }

            m_appearance.sprite = m_lstSpriteSheet[l_dSpriteIndex];

            m_appearance.color = m_lstColor[l_dColorIndex];
        }
    }
}
