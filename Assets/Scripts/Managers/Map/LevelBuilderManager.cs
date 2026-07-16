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
        private ItemBricksUtility l_itemBricksUtility;

        public LevelBuilderManager(ScriptableLevelConfiguration p_levelConfig)
        {
            GridCellBehaviour[] l_lstCells = Object.FindObjectsByType<GridCellBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            foreach (GridCellBehaviour l_cell in l_lstCells)
            {
                l_cell.Init();
            }

            BuildLevel(p_levelConfig);
        }

        public void BuildLevel(ScriptableLevelConfiguration p_levelConfig)
        {
            PlaceHayRollStacks();

            List<int> l_lstEnabledTowers = EnableTowers(p_levelConfig);

            InitSwitchableParts(p_levelConfig.AllPossibleParts, l_lstEnabledTowers);

            VillagersSpawnManager.Instance.Init(l_lstEnabledTowers, p_levelConfig.AllWaves);
        }

        private List<int> EnableTowers(ScriptableLevelConfiguration p_levelConfig)
        {
            TowerBehaviour[] l_lstTowers = Object.FindObjectsByType<TowerBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            int l_dTowerCount = l_lstTowers.Length;

            bool l_bUseMutltipleOfTwo = MathUtils.HeadsOrTails();

            bool l_bUseAscendingOrder = MathUtils.HeadsOrTails();

            List<int> l_lstEnabledSpawners = new List<int>();

            int l_dMainSpawnerCount = p_levelConfig.MainSpawnerCount;

            l_itemBricksUtility = new ItemBricksUtility(p_levelConfig.BricksCount, l_lstTowers.Length);

            if (l_bUseAscendingOrder)
            {
                for (int l_i = 0; l_i < l_dTowerCount; l_i++)
                {
                    TowerBehaviour l_tower = l_lstTowers[l_i];

                    EnableBricksOnWalls(l_tower);

                    if (l_dMainSpawnerCount == l_dTowerCount)
                    {
                        l_tower.enabled = true;
                    }
                    else
                    {
                        if (l_bUseMutltipleOfTwo && l_i % 2 == 0 && l_dMainSpawnerCount > 0)
                        {
                            l_tower.enabled = true;

                            l_dMainSpawnerCount -= 1;
                        }
                        else if (!l_bUseMutltipleOfTwo && l_i % 2 != 0 && l_dMainSpawnerCount > 0)
                        {
                            l_tower.enabled = true;

                            l_dMainSpawnerCount -= 1;
                        }
                        else
                        {
                            if (l_dMainSpawnerCount == l_dTowerCount - 1)
                            {
                                l_tower.enabled = MathUtils.HeadsOrTails();

                                if (l_tower.enabled == true)
                                {
                                    l_dMainSpawnerCount -= 1;
                                }
                            }
                            else if (l_dMainSpawnerCount == 1)
                            {
                                l_tower.enabled = true;
                            }
                            else
                            {
                                l_tower.enabled = false;
                            }
                        }
                    }

                    if (l_tower.enabled)
                    {
                        l_lstEnabledSpawners.Add(l_tower.SpawnerId);
                    }
                }
            }
            else
            {
                for (int l_i = l_dTowerCount - 1; l_i >= 0; l_i--)
                {
                    TowerBehaviour l_tower = l_lstTowers[l_i];

                    EnableBricksOnWalls(l_tower);

                    if (l_dMainSpawnerCount == l_dTowerCount)
                    {
                        l_tower.enabled = true;
                    }
                    else
                    {
                        if (l_bUseMutltipleOfTwo && l_i % 2 == 0 && l_dMainSpawnerCount > 0)
                        {
                            l_tower.enabled = true;

                            l_dMainSpawnerCount -= 1;
                        }
                        else if (!l_bUseMutltipleOfTwo && l_i % 2 != 0 && l_dMainSpawnerCount > 0)
                        {
                            l_tower.enabled = true;

                            l_dMainSpawnerCount -= 1;
                        }
                        else
                        {
                            if (l_dMainSpawnerCount == l_dTowerCount - 1)
                            {
                                l_tower.enabled = MathUtils.HeadsOrTails();

                                if (l_tower.enabled == true)
                                {
                                    l_dMainSpawnerCount -= 1;
                                }
                            }
                            else if (l_dMainSpawnerCount == 1)
                            {
                                l_tower.enabled = true;
                            }
                            else
                            {
                                l_tower.enabled = false;
                            }
                        }
                    }

                    if (l_tower.enabled)
                    {
                        l_lstEnabledSpawners.Add(l_lstTowers[l_i].SpawnerId);
                    }
                }
            }

            return l_lstEnabledSpawners;
        }

        private void EnableBricksOnWalls(TowerBehaviour p_tower)
        {
            int[] l_lstBrickCounts = new int[2];

            for (int l_j = 0; l_j <= 1; l_j++)
            {
                l_lstBrickCounts[l_j] = l_itemBricksUtility.Pick();
            }

            p_tower.EnableItemBricks(l_lstBrickCounts);
        }

        private void InitSwitchableParts(List<SwitchablePartCount> p_lstSwitchablePartCount, List<int> p_lstEnabledTowers)
        {
            SwitchablePartBehaviour[] l_lstSwitchableParts = Object.FindObjectsByType<SwitchablePartBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None);

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
            HayRollBehaviour[] l_lstHayRolls = Object.FindObjectsByType<HayRollBehaviour>(FindObjectsSortMode.None);

            foreach (HayRollBehaviour l_hayRoll in l_lstHayRolls)
            {
                PlayerInventoryManager.Instance.HoldItem(l_hayRoll);

                GridCellBehaviour l_cell = ItemPlacerManager.Instance.GetCellToPlaceHayRoll(l_hayRoll);

                PlayerInventoryManager.Instance.PlaceHeldItem(l_cell);
            }
        }
    }
}
