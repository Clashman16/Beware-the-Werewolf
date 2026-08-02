using System.Collections.Generic;

namespace BWW.Utils.Map
{
    public class ItemBricksUtility : PickingUtility
    {
        private int m_dItemBricksCount;

        private int m_dItemBricksCountMax;

        private bool m_bBricksOnLastWall;

        private int m_dWallCount;

        private int m_dWallCountMax;

        public ItemBricksUtility(int p_dItemBricksCount, int p_dTowerCount) : base()
        {
            m_dItemBricksCount = p_dItemBricksCount;

            m_dItemBricksCountMax = p_dItemBricksCount;

            PossiblePicks.AddRange(new List<int>() { 1, 2});

            if (m_dItemBricksCount != 4)
            {
                PossiblePicks.Add(3);
            }

            m_bBricksOnLastWall = MathUtils.HeadsOrTails();

            m_dWallCount = p_dTowerCount * 2;

            m_dWallCountMax = p_dTowerCount * 2;
        }

        public override int Pick()
        {
            if (m_dItemBricksCount > 0 &&  m_dWallCount > 0)
            {
                if(m_bBricksOnLastWall)
                {
                    if(m_dWallCount == 1 && m_dItemBricksCount > 0)
                    {
                        m_bBricksOnLastWall = true;
                    }
                    else
                    {
                        m_bBricksOnLastWall = MathUtils.HeadsOrTails();
                    }
                }
                else
                {
                    m_bBricksOnLastWall = true;
                }

                if(m_bBricksOnLastWall)
                {
                    while(m_dItemBricksCount < PossiblePicks.Count)
                    {
                        RemovePossibilityWithIndex(PossiblePicks.Count - 1);
                    }

                    int l_dEnabledBricksCount;

                    if(m_dWallCount == 1)
                    {
                        l_dEnabledBricksCount = PossiblePicks[PossiblePicks.Count - 1];
                    }
                    else if(PossiblePicks.Count == 1)
                    {
                        l_dEnabledBricksCount = 1;
                    }
                    else
                    {
                        bool l_bPossiblePicksCountEqualThree = PossiblePicks.Count == 3;

                        if (PossiblePicks.Count == 3)
                        {
                            int l_dIndexToRemove = GetProbabilityToPlaceFewBricks() > 0.5f ? PossiblePicks.Count - 1 : 0;

                            l_dEnabledBricksCount = TemporaryRemoveAndPick(l_dIndexToRemove);
                        }
                        else
                        {
                            l_dEnabledBricksCount = PossiblePicks[GetRandomPickId()];
                        }
                    }

                    m_dWallCount -= 1;

                    m_dItemBricksCount -= l_dEnabledBricksCount;

                    return l_dEnabledBricksCount;
                }
            }

            return 0;
        }

        private void RemovePossibilityWithIndex(int p_dIndex)
        {
            PossiblePicks.RemoveAt(p_dIndex);
        }

        private float GetProbabilityToPlaceFewBricks()
        {
            return (float)(m_dItemBricksCountMax - m_dItemBricksCount) / (float)(m_dWallCountMax - m_dWallCount);
        }

        private int TemporaryRemoveAndPick(int p_dIndexToRemove)
        {
            int l_dPickValue = PossiblePicks[p_dIndexToRemove];

            RemovePossibilityWithIndex(p_dIndexToRemove);

            int l_dEnabledBricksCount = PossiblePicks[GetRandomPickId()];

            PossiblePicks.Insert(p_dIndexToRemove, l_dPickValue);

            return l_dEnabledBricksCount;
        }
    }
}
