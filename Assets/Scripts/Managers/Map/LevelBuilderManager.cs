using BWW.Behaviours.Map;
using BWW.Behaviours.Map.Items;
using BWW.Managers.Player;
using BWW.ScriptableObjects.Map;
using BWW.Utils;
using BWW.Utils.Map;
using System.Collections.Generic;
using UnityEngine;

namespace BWW.Managers.Map
{
   public class LevelBuilderManager
   {
      public LevelBuilderManager(ScriptableLevelConfiguration p_levelConfig)
      {
         GridCellBehaviour[] l_lstCells = Object.FindObjectsByType<GridCellBehaviour>(FindObjectsInactive.Include);

         foreach(GridCellBehaviour l_cell in l_lstCells)
         {
            l_cell.Init();
         }

         BuildLevel(p_levelConfig);
      }

      public void BuildLevel(ScriptableLevelConfiguration p_levelConfig)
      {
         PlaceHayRollStacks();

         List<int> l_lstEnabledTowers = EnableTowers(p_levelConfig.MainSpawnerCount);

         InitSwitchableParts(p_levelConfig.AllPossibleParts, l_lstEnabledTowers);

         VillagersSpawnManager.Instance.Init(l_lstEnabledTowers, p_levelConfig.AllWaves);
      }

      private List<int> EnableTowers(int p_mainSpawnerCount)
      {
         TowerBehaviour[] l_lstSpawners = Object.FindObjectsByType<TowerBehaviour>(FindObjectsInactive.Include);

         int l_dTowerCount = l_lstSpawners.Length;

         bool l_bUseMutltipleOfTwo = MathUtils.HeadsOrTails();

         bool l_bUseAscendingOrder = MathUtils.HeadsOrTails();

         List<int> l_lstEnabledSpawners = new List<int>();

         if (l_bUseAscendingOrder)
         {
            for (int l_i = 0; l_i < l_dTowerCount; l_i++)
            {
               if (p_mainSpawnerCount == l_dTowerCount)
               {
                  l_lstSpawners[l_i].enabled = true;
               }
               else
               {
                  if (l_bUseMutltipleOfTwo && l_i % 2 == 0 && p_mainSpawnerCount > 0)
                  {
                     l_lstSpawners[l_i].enabled = true;

                     p_mainSpawnerCount -= 1;
                  }
                  else if (!l_bUseMutltipleOfTwo && l_i % 2 != 0 && p_mainSpawnerCount > 0)
                  {
                     l_lstSpawners[l_i].enabled = true;

                     p_mainSpawnerCount -= 1;
                  }
                  else
                  {
                     if (p_mainSpawnerCount == l_dTowerCount - 1)
                     {
                        l_lstSpawners[l_i].enabled = MathUtils.HeadsOrTails();

                        if (l_lstSpawners[l_i].enabled == true)
                        {
                           p_mainSpawnerCount -= 1;
                        }
                     }
                     else if (p_mainSpawnerCount == 1)
                     {
                        l_lstSpawners[l_i].enabled = true;
                     }
                     else
                     {
                        l_lstSpawners[l_i].enabled = false;
                     }
                  }
               }

               if (l_lstSpawners[l_i].enabled)
               {
                  l_lstEnabledSpawners.Add(l_lstSpawners[l_i].SpawnerId);
               }
            }
         }
         else
         {
            for (int l_i = l_dTowerCount - 1; l_i >= 0; l_i--)
            {
               if (p_mainSpawnerCount == l_dTowerCount)
               {
                  l_lstSpawners[l_i].enabled = true;
               }
               else
               {
                  if (l_bUseMutltipleOfTwo && l_i % 2 == 0 && p_mainSpawnerCount > 0)
                  {
                     l_lstSpawners[l_i].enabled = true;

                     p_mainSpawnerCount -= 1;
                  }
                  else if (!l_bUseMutltipleOfTwo && l_i % 2 != 0 && p_mainSpawnerCount > 0)
                  {
                     l_lstSpawners[l_i].enabled = true;

                     p_mainSpawnerCount -= 1;
                  }
                  else
                  {
                     if (p_mainSpawnerCount == l_dTowerCount - 1)
                     {
                        l_lstSpawners[l_i].enabled = MathUtils.HeadsOrTails();

                        if (l_lstSpawners[l_i].enabled == true)
                        {
                           p_mainSpawnerCount -= 1;
                        }
                     }
                     else if (p_mainSpawnerCount == 1)
                     {
                        l_lstSpawners[l_i].enabled = true;
                     }
                     else
                     {
                        l_lstSpawners[l_i].enabled = false;
                     }
                  }
               }

               if (l_lstSpawners[l_i].enabled)
               {
                  l_lstEnabledSpawners.Add(l_lstSpawners[l_i].SpawnerId);
               }
            }
         }

         return l_lstEnabledSpawners;
      }

      private void InitSwitchableParts(List<SwitchablePartCount> p_lstSwitchablePartCount, List<int> p_lstEnabledTowers)
      {
         SwitchablePartBehaviour[] l_lstSwitchableParts = Object.FindObjectsByType<SwitchablePartBehaviour>(FindObjectsInactive.Include);

         int l_dSwitchablePartsCount = l_lstSwitchableParts.Length;

         bool l_bUseMutltipleOfTwo = MathUtils.HeadsOrTails();

         bool l_bUseAscendingOrder = MathUtils.HeadsOrTails();

         SwitchablePartUtility l_utility = null;

         if (l_bUseAscendingOrder)
         {
            for (int l_i = 0; l_i < l_dSwitchablePartsCount; l_i++)
            {
               SwitchablePartBehaviour l_switchablePart = l_lstSwitchableParts[l_i];

               if (l_utility == null)
               {
                  l_utility = new SwitchablePartUtility(l_switchablePart, p_lstSwitchablePartCount);
               }
               else
               {
                  l_utility.SwitchablePart = l_switchablePart;
               }

               bool l_bHasSwitched = false;

               if (l_i == l_dSwitchablePartsCount - 1)
               {
                  l_bHasSwitched = l_utility.SwitchPartToStairs(true);
               }

               if (!l_bHasSwitched)
               {
                  if (l_bUseMutltipleOfTwo && l_i % 2 == 0)
                  {
                     bool l_bCloseToSpawner = l_switchablePart.IsCloseToSpawner(p_lstEnabledTowers);

                     if (l_bCloseToSpawner)
                     {
                        l_bHasSwitched = l_utility.SwitchPartToStairs();
                     }

                     if (!l_bHasSwitched)
                     {
                        l_utility.SwitchPartToRandom(l_bCloseToSpawner);
                     }
                  }
                  else if (!l_bUseMutltipleOfTwo && l_i % 2 != 0)
                  {
                     bool l_bCloseToSpawner = l_switchablePart.IsCloseToSpawner(p_lstEnabledTowers);

                     if (l_bCloseToSpawner)
                     {
                        l_bHasSwitched = l_utility.SwitchPartToStairs();
                     }

                     if (!l_bHasSwitched)
                     {
                        l_utility.SwitchPartToRandom(l_bCloseToSpawner);
                     }
                  }
                  else
                  {
                     bool l_bCloseToSpawner = l_switchablePart.IsCloseToSpawner(p_lstEnabledTowers);

                     if (l_bCloseToSpawner)
                     {
                        l_bHasSwitched = l_utility.SwitchPartToStairs();
                     }

                     if (!l_bHasSwitched)
                     {
                        l_utility.SwitchPartToRandom(l_bCloseToSpawner);
                     }
                  }
               }
            }
         }
         else
         {
            for (int l_i = l_dSwitchablePartsCount - 1; l_i >= 0; l_i--)
            {
               SwitchablePartBehaviour l_switchablePart = l_lstSwitchableParts[l_i];

               if (l_utility == null)
               {
                  l_utility = new SwitchablePartUtility(l_switchablePart, p_lstSwitchablePartCount);
               }
               else
               {
                  l_utility.SwitchablePart = l_switchablePart;
               }

               bool l_bHasSwitched = false;

               if (l_i == 0)
               {
                  l_bHasSwitched = l_utility.SwitchPartToStairs(true);
               }

               if (!l_bHasSwitched)
               {
                  if (l_bUseMutltipleOfTwo && l_i % 2 == 0)
                  {
                     bool l_bCloseToSpawner = l_switchablePart.IsCloseToSpawner(p_lstEnabledTowers);

                     if (l_bCloseToSpawner)
                     {
                        l_bHasSwitched = l_utility.SwitchPartToStairs();
                     }

                     if (!l_bHasSwitched)
                     {
                        l_utility.SwitchPartToRandom(l_bCloseToSpawner);
                     }
                  }
                  else if (!l_bUseMutltipleOfTwo && l_i % 2 != 0)
                  {
                     bool l_bCloseToSpawner = l_switchablePart.IsCloseToSpawner(p_lstEnabledTowers);

                     if (l_bCloseToSpawner)
                     {
                        l_bHasSwitched = l_utility.SwitchPartToStairs();
                     }

                     if (!l_bHasSwitched)
                     {
                        l_utility.SwitchPartToRandom(l_bCloseToSpawner);
                     }
                  }
                  else
                  {
                     bool l_bCloseToSpawner = l_switchablePart.IsCloseToSpawner(p_lstEnabledTowers);

                     if (l_bCloseToSpawner)
                     {
                        l_bHasSwitched = l_utility.SwitchPartToStairs();
                     }

                     if (!l_bHasSwitched)
                     {
                        l_utility.SwitchPartToRandom(l_bCloseToSpawner);
                     }
                  }
               }
            }
         }
      }

      private void PlaceHayRollStacks()
      {
         HayRollBehaviour[] l_lstHayRolls = Object.FindObjectsByType<HayRollBehaviour>();

         foreach (HayRollBehaviour l_hayRoll in l_lstHayRolls)
         {
            PlayerInventoryManager.Instance.HoldItem(l_hayRoll);

            GridCellBehaviour l_cell = ItemPlacerManager.Instance.GetCellToPlaceHayRoll(l_hayRoll);

            PlayerInventoryManager.Instance.PlaceHeldItem(l_cell);
         }
      }
   }
}
