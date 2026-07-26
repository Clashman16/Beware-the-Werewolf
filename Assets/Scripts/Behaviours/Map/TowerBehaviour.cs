using BWW.Behaviours.Map.Items;
using BWW.Enums;
using BWW.Utils;
using UnityEngine;

namespace BWW.Behaviours.Map
{
    public class TowerBehaviour : GateSpawnerBehaviour
    {
        [SerializeField] private GameObject[] m_lstWalls;

        public override GameObject InstantiateVillager(EVillagerType p_eEnemyType)
        {
            GameObject l_goVillager = base.InstantiateVillager(p_eEnemyType);

            l_goVillager.transform.SetParent(transform.GetChild(2), true);

            l_goVillager.transform.localPosition = Vector3.zero;

            l_goVillager.transform.parent = null;

            return l_goVillager;
        }

        public void EnableItemBricks(int[] p_lstBrickCounts)
        {
            for(int l_i = 0; l_i <= 1; l_i++)
            {
                int l_dBrickCount = p_lstBrickCounts[l_i];

                if (l_dBrickCount > 0)
                {
                    ResourceItem[] l_lstBricks = m_lstWalls[l_i].GetComponentsInChildren<ResourceItem>(true);

                    bool l_bUseAscendingOrder = MathUtils.HeadsOrTails();

                    int l_dStartIndex = l_bUseAscendingOrder ? 0 : l_dBrickCount - 1;

                    int l_dIncrementValue = l_bUseAscendingOrder ? 1 : -1;

                    int l_dLoopStopIndex = l_bUseAscendingOrder ? l_dBrickCount : -1;

                    for (; l_dStartIndex != l_dLoopStopIndex; l_dStartIndex += l_dIncrementValue)
                    {
                        l_lstBricks[l_dStartIndex].gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}
