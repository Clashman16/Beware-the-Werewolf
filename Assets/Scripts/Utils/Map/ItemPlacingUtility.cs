using BWW.Behaviours.Map;
using BWW.Behaviours.Map.Items;
using BWW.Managers.Map;
using BWW.Utils;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlacingUtility
{
   public MovableItem ShowItem(bool p_bIsVisible, GridCellBehaviour p_cell, string p_sItemKey = "")
   {
      if(p_bIsVisible)
      {
         GameObject l_goPlacedItem = null;

         switch(p_sItemKey)
         {
            default:

               l_goPlacedItem = GetWoodOrBricksPart(p_cell, p_sItemKey);

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

      foreach(int l_dNeighborId in p_cell.Neighbors.Keys)
      {
         GridCellBehaviour l_neighbor = p_cell.Neighbors[l_dNeighborId];

         if (l_neighbor.PlacedItem != null && l_neighbor.PlacedItem.name == p_sItemKey)
         {
            l_firstNeighbor = l_neighbor;

            break;
         }
      }

      if(l_firstNeighbor == null)
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

         if(MathUtils.HeadsOrTails())
         {
            l_neigborIndexModificators.Sort((l_dA, l_dB) => l_dB.CompareTo(l_dA));
         }

         foreach (int l_dModificator in l_neigborIndexModificators)
         {
            GridCellBehaviour l_neighbor = p_cell.Neighbors[p_cell.CellIndex+ l_dModificator];

            if (l_neighbor.PlacedItem != null && l_neighbor.PlacedItem.name == p_sItemKey)
            {
               int l_dFirstPartRotationCount = (int) l_firstNeighbor.PlacedItem.transform.rotation.eulerAngles.x / 90;

               int l_dScndPartRotationCount = (int)l_neighbor.PlacedItem.transform.rotation.eulerAngles.x / 90;

               if (l_dFirstPartRotationCount % 2 == 0 && l_dScndPartRotationCount % 2 != 0
                  || (l_dFirstPartRotationCount % 2 != 0 && l_dScndPartRotationCount % 2 == 0))
               {
                  l_scndNeighbor = l_neighbor;

                  break;
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
            GridCellBehaviour l_neighbor = p_cell.Neighbors[p_cell.CellIndex + l_dModificator];

            if (l_neighbor.PlacedItem != null && l_neighbor.PlacedItem.name == p_sItemKey)
            {
               int l_dFirstPartRotationCount = (int)l_firstNeighbor.PlacedItem.transform.rotation.eulerAngles.x / 90;

               int l_dScndPartRotationCount = (int)l_neighbor.PlacedItem.transform.rotation.eulerAngles.x / 90;

               if (l_dFirstPartRotationCount % 2 == 0 && l_dScndPartRotationCount % 2 != 0
                  || (l_dFirstPartRotationCount % 2 != 0 && l_dScndPartRotationCount % 2 == 0))
               {
                  l_scndNeighbor = l_neighbor;

                  break;
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

         switch(l_dSum)
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

         l_trfCurve.RotateAround(l_trfCurve.position, Vector3.up, l_dAngle*90);

         return l_trfCurve.gameObject;
      }
   }
}
