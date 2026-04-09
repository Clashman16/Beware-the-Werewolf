using BWW.Behaviours.Map;
using BWW.Behaviours.Map.Items;
using BWW.Utils;
using BWW.Utils.Map;
using System.Collections.Generic;
using UnityEngine;

namespace BWW.Managers.Map
{
   public sealed class ItemPlacerManager
   {
      private static ItemPlacerManager m_instance;

      public static ItemPlacerManager Instance
      {
         get
         {
            if (m_instance == null)
            {
               m_instance = new ItemPlacerManager();
            }
            return m_instance;
         }
      }

      private ItemPlacingUtility m_utility;

      private Dictionary<int, List<GridCellBehaviour>> m_lstHayRollPossiblePlaces;

      private ItemPlacerManager()
      {
         m_utility = new ItemPlacingUtility();

         m_lstHayRollPossiblePlaces = new Dictionary<int, List<GridCellBehaviour>> ();

         for(int l_i = 0; l_i <= 2; l_i++)
         {
            m_lstHayRollPossiblePlaces.Add(l_i, new List<GridCellBehaviour>());
         }

         GridCellBehaviour[] l_lstCells = Object.FindObjectsByType<GridCellBehaviour>();

         foreach (GridCellBehaviour l_cell in l_lstCells)
         {
            if (m_utility.CanPlaceHayRollStackOnCell(l_cell, 0))
            {
               int l_dHayRollStackCount = m_utility.GetHayRollStackCountOnCell(l_cell);

               m_lstHayRollPossiblePlaces[l_dHayRollStackCount].Add(l_cell);
            }
         }
      }

      public MovableItem PlaceItem(string p_sItemKey, GridCellBehaviour p_cell)
      {
         return m_utility.ShowItem(true, p_cell, p_sItemKey);
      }

      public MovableItem TakeItem(string p_sItemKey, GridCellBehaviour p_cell)
      {
         return m_utility.ShowItem(false, p_cell);
      }

      public GridCellBehaviour GetCellToPlaceHayRoll(HayRollBehaviour p_hayRoll)
      {
         GridCellBehaviour l_lastCell = p_hayRoll.GetComponentInParent<GridCellBehaviour>();

         int l_dStackCount = m_utility.GetHayRollStackCount(p_hayRoll);

         int l_dStackCountOnCell;

         switch(l_dStackCount)
         {
            case 1:

               if (MathUtils.HeadsOrTails())
               {
                  l_dStackCountOnCell = GetStackCountOnCell(0, 1);
               }
               else
               {
                  l_dStackCountOnCell = GetStackCountOnCell(2, -1);
               }

               break;

            case 2:

               if (MathUtils.HeadsOrTails())
               {
                  l_dStackCountOnCell = GetStackCountOnCell(0, 1);
               }
               else
               {
                  l_dStackCountOnCell = GetStackCountOnCell(1, -1);
               }

               break;

            default:

               l_dStackCountOnCell = 0;

               break;
         }

         List<GridCellBehaviour> l_lstPossiblePlaces = m_lstHayRollPossiblePlaces[l_dStackCountOnCell];

         int l_dCellIndex = Random.Range(0, l_lstPossiblePlaces.Count);

         GridCellBehaviour l_cell = l_lstPossiblePlaces[l_dCellIndex];

         m_lstHayRollPossiblePlaces[l_dStackCountOnCell].Remove(l_cell);

         int l_dNewStackCountOnCell = l_dStackCountOnCell + l_dStackCount;

         if(l_dNewStackCountOnCell < 3 && m_utility.CanPlaceHayRollStackOnCell(l_cell, l_dStackCount))
         {
            m_lstHayRollPossiblePlaces[l_dNewStackCountOnCell].Add(l_cell);
         }

         return l_cell;
      }

      private int GetStackCountOnCell(int p_dStartIterationValue, int p_dIncrement)
      {
         int l_dStackCountOnCell = -1;

         while (l_dStackCountOnCell == -1)
         {
            if (m_lstHayRollPossiblePlaces[p_dStartIterationValue].Count > 0)
            {
               l_dStackCountOnCell = p_dStartIterationValue;
            }
            else
            {
               p_dStartIterationValue += p_dIncrement;
            }
         }

         return l_dStackCountOnCell;
      }
   }
}
