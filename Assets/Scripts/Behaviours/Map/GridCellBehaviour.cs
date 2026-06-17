using BWW.Behaviours.Map.Items;
using BWW.Enums;
using BWW.Managers.Map;
using System.Collections.Generic;
using UnityEngine;

namespace BWW.Behaviours.Map
{
   public class GridCellBehaviour : MonoBehaviour
   {
      private MovableItem m_placedItem;

      public MovableItem PlacedItem
      {
         get => m_placedItem;
         set => m_placedItem = value;
      }

      private int m_dCellIndex;

      public int CellIndex
      {
         get => m_dCellIndex;
      }

      private Dictionary<int, GridCellBehaviour> m_lstNeighbors;

      public Dictionary<int,GridCellBehaviour> Neighbors
      {
         get => m_lstNeighbors;
      }

        private EGridCellState m_eState;

        public EGridCellState State
        {
            get => m_eState;
            set => m_eState = value;
        }

      /**private void Start()
      {
         Init();
      }**/

      public void Init()
      {
            m_eState = EGridCellState.NORMAL;

         m_lstNeighbors = new Dictionary<int, GridCellBehaviour>();

         char[] l_lstSeparator = new char[] { '(', ')' };

         m_dCellIndex = int.Parse(name.Split(l_lstSeparator)[1]);

         int l_dGridSize = GridManager.Instance.GridSize;

         int[] l_neigborIndexModificators = new int[] { -1, 1, -l_dGridSize, l_dGridSize };

         foreach (int l_dModificator in l_neigborIndexModificators)
         {
            int l_dNeighborIndex = m_dCellIndex + l_dModificator;

            Transform l_trfNeighbor = transform.parent.Find($"GridCell ({l_dNeighborIndex})");

            if (l_trfNeighbor != null)
            {
               m_lstNeighbors.Add(l_dNeighborIndex, l_trfNeighbor.GetComponent<GridCellBehaviour>());
            }
         }
      }

      public void PlaceItem(string p_sItemKey)
      {
         MovableItem l_newItem = ItemPlacerManager.Instance.PlaceItem(p_sItemKey, this);

         if (m_placedItem == null)
         {
            m_placedItem = l_newItem;
         }
      }

      public void TakeItem()
      {
         ItemPlacerManager.Instance.TakeItem(m_placedItem.name, this);

         m_placedItem = null;

         transform.localRotation = Quaternion.Euler(0, 0, 0);
      }
   }
}
