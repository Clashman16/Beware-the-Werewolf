using BWW.Behaviours.Map;
using BWW.Enums;
using BWW.ScriptableObjects.Map;
using System.Collections.Generic;
using UnityEngine;

namespace BWW.Utils.Map
{
    public class SwitchablePartUtility
    {
        SwitchablePartBehaviour m_switchablePart;

        public SwitchablePartBehaviour SwitchablePart
        {
            set => m_switchablePart = value;
        }

        Dictionary<ESwitchablePart, int> m_lstSwitchablePartTypes;

        public SwitchablePartUtility(SwitchablePartBehaviour p_switchablePart, List<SwitchablePartCount> p_lstSwitchablePartCount)
        {
            m_switchablePart = p_switchablePart;

            m_lstSwitchablePartTypes = new Dictionary<ESwitchablePart, int>();

            foreach (SwitchablePartCount l_count in p_lstSwitchablePartCount)
            {
                m_lstSwitchablePartTypes.Add(l_count.Part, l_count.Count);
            }
        }

        public bool SwitchPartToStairs(bool p_bForceToSpawnStairs = false)
        {
            if (m_lstSwitchablePartTypes.ContainsKey(ESwitchablePart.STAIRS))
            {
                bool l_bSpawnStairs = MathUtils.HeadsOrTails();

                if (l_bSpawnStairs || p_bForceToSpawnStairs)
                {
                    SwitchPart(ESwitchablePart.STAIRS);

                    return true;
                }
            }

            return false;
        }


        public void SwitchPartToRandom(bool p_bCloseToSpawner)
        {
            List<ESwitchablePart> l_lstSwitchablePartTypesTemp = new List<ESwitchablePart>();

            foreach (KeyValuePair<ESwitchablePart, int> l_pair in m_lstSwitchablePartTypes)
            {
                if (!(l_pair.Key == ESwitchablePart.STAIRS && !p_bCloseToSpawner))
                {
                    l_lstSwitchablePartTypesTemp.Add(l_pair.Key);
                }
            }

            int l_randomSwitchablePartId = Random.Range(0, l_lstSwitchablePartTypesTemp.Count);

            ESwitchablePart l_eRandomPartType = l_lstSwitchablePartTypesTemp[l_randomSwitchablePartId];

            SwitchPart(l_eRandomPartType);
        }

        public void SwitchPart(ESwitchablePart p_ePart)
        {
            m_switchablePart.Switch(p_ePart);

            m_lstSwitchablePartTypes[p_ePart] -= 1;

            if (m_lstSwitchablePartTypes[p_ePart] == 0)
            {
                m_lstSwitchablePartTypes.Remove(p_ePart);
            }
        }
    }
}

