using BWW.Behaviours.Map;
using BWW.Behaviours.Map.Items;
using BWW.Managers.Map;
using BWW.Managers.Player;
using System.Collections.Generic;
using UnityEngine;

namespace BWW.Utils.Map
{
   public class ItemPlacingUtility
   {
      public MovableItem ShowItem(bool p_bIsVisible, GridCellBehaviour p_cell, string p_sItemKey = "")
      {
         if (p_bIsVisible)
         {
            GameObject l_goPlacedItem = null;

            switch (p_sItemKey)
            {
               case "Bricks":
               case "Wood":

                  l_goPlacedItem = GetWoodOrBricksPart(p_cell, p_sItemKey);

                  break;

               default:

                  l_goPlacedItem = GetHayRollStack(p_cell);

                  break;
            }

            l_goPlacedItem.SetActive(true);

            return l_goPlacedItem.GetComponent<MovableItem>();
         }
         else
         {
            p_cell.PlacedItem.gameObject.SetActive(false);

            return p_cell.PlacedItem;
         }
      }

      private GameObject GetWoodOrBricksPart(GridCellBehaviour p_cell, string p_sItemKey)
      {
         GridCellBehaviour l_firstNeighbor = null;

         foreach (int l_dNeighborId in p_cell.Neighbors.Keys)
         {
            GridCellBehaviour l_neighbor = p_cell.Neighbors[l_dNeighborId];

            if (l_neighbor.PlacedItem != null && l_neighbor.PlacedItem.name == p_sItemKey)
            {
               l_firstNeighbor = l_neighbor;

               break;
            }
         }

         if (l_firstNeighbor == null)
         {
            return p_cell.transform.Find(p_sItemKey).gameObject;
         }

         GridCellBehaviour l_scndNeighbor = null;

         int l_dGridSize = GridManager.Instance.GridSize;

         List<int> l_neigborIndexModificators = new List<int>();

         if (l_firstNeighbor.CellIndex % GridManager.Instance.GridSize == 0)
         {
            l_neigborIndexModificators.Add(-1);
            l_neigborIndexModificators.Add(1);

            if (MathUtils.HeadsOrTails())
            {
               l_neigborIndexModificators.Sort((l_dA, l_dB) => l_dB.CompareTo(l_dA));
            }

            foreach (int l_dModificator in l_neigborIndexModificators)
            {
               int l_dNewIndex = p_cell.CellIndex + l_dModificator;

               if (p_cell.Neighbors.ContainsKey(l_dNewIndex))
               {
                  GridCellBehaviour l_neighbor = p_cell.Neighbors[l_dNewIndex];

                  if (l_neighbor.PlacedItem != null && l_neighbor.PlacedItem.name == p_sItemKey)
                  {
                     int l_dFirstPartRotationCount = (int)l_firstNeighbor.PlacedItem.transform.localEulerAngles.x / 90;

                     int l_dScndPartRotationCount = (int)l_neighbor.PlacedItem.transform.localEulerAngles.x / 90;

                     if (l_dFirstPartRotationCount % 2 == 0 && l_dScndPartRotationCount % 2 != 0
                        || (l_dFirstPartRotationCount % 2 != 0 && l_dScndPartRotationCount % 2 == 0))
                     {
                        l_scndNeighbor = l_neighbor;

                        break;
                     }
                  }
               }

            }
         }
         else
         {
            l_neigborIndexModificators.Add(-l_dGridSize);
            l_neigborIndexModificators.Add(l_dGridSize);

            if (MathUtils.HeadsOrTails())
            {
               l_neigborIndexModificators.Sort((l_dA, l_dB) => l_dB.CompareTo(l_dA));
            }

            foreach (int l_dModificator in l_neigborIndexModificators)
            {
               int l_dNewIndex = p_cell.CellIndex + l_dModificator;

               if (p_cell.Neighbors.ContainsKey(l_dNewIndex))
               {
                  GridCellBehaviour l_neighbor = p_cell.Neighbors[p_cell.CellIndex + l_dModificator];

                  if (l_neighbor.PlacedItem != null && l_neighbor.PlacedItem.name == p_sItemKey)
                  {
                     int l_dFirstPartRotationCount = (int)l_firstNeighbor.PlacedItem.transform.localEulerAngles.x / 90;

                     int l_dScndPartRotationCount = (int)l_neighbor.PlacedItem.transform.localEulerAngles.x / 90;

                     if (l_dFirstPartRotationCount % 2 == 0 && l_dScndPartRotationCount % 2 != 0
                        || (l_dFirstPartRotationCount % 2 != 0 && l_dScndPartRotationCount % 2 == 0))
                     {
                        l_scndNeighbor = l_neighbor;

                        break;
                     }
                  }
               }
            }
         }

         if (l_scndNeighbor == null)
         {
            return p_cell.transform.Find(p_sItemKey).gameObject;
         }
         else
         {
            Transform l_trfCurve = p_cell.transform.Find(p_sItemKey + "Curve");

            int l_dFirstModificator = l_firstNeighbor.CellIndex - p_cell.CellIndex;

            int l_dScndModificator = l_scndNeighbor.CellIndex - p_cell.CellIndex;

            int l_dSum = l_dFirstModificator + l_dScndModificator;

            int l_dAngle;

            switch (l_dSum)
            {
               case -9:

                  l_dAngle = -3;

                  break;
               case -7:

                  l_dAngle = -2;

                  break;
               case 7:

                  l_dAngle = 0;

                  break;

               default:

                  l_dAngle = -1;

                  break;
            }

            l_trfCurve.RotateAround(l_trfCurve.position, Vector3.up, l_dAngle * 90);

            return l_trfCurve.gameObject;
         }
      }

      private GameObject GetHayRollStack(GridCellBehaviour p_cell)
      {
         GameObject l_goHayRollStack = PlayerInventoryManager.Instance.HeldItem.gameObject;

         HayRollBehaviour[] l_lstHayRolls = p_cell.GetComponentsInChildren<HayRollBehaviour>();

         Transform l_trfParent;

         Vector3 l_vecPosition;

         Vector3 l_vecRotation;

         switch (l_lstHayRolls.Length)
         {
            case 1:

               l_trfParent = p_cell.transform.GetComponentInChildren<HayRollBehaviour>().transform;

               l_vecPosition = new Vector3(0, 0.257f, 0);

               l_vecRotation = Vector3.zero;

               break;

            case 2:

               l_trfParent = p_cell.transform.GetComponentInChildren<HayRollBehaviour>().transform.GetChild(0);

               l_vecPosition = new Vector3(0, 0.257f, 0);

               l_vecRotation = Vector3.zero;

               break;

            default:

               l_trfParent = p_cell.transform;

               l_vecPosition = new Vector3(0, 0, 0.0163f);

               l_vecRotation = new Vector3(-90, 0, 0);

               break;
         }

         Transform l_trfStack = l_goHayRollStack.transform;

         l_trfStack.SetParent(l_trfParent);

         l_trfStack.localPosition = l_vecPosition;

         l_trfStack.localRotation = Quaternion.Euler(l_vecRotation);

         return l_goHayRollStack;
      }
   }
}
