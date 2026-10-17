using BWW.Utils;
using UnityEngine;

namespace BWW.Behaviours.Map
{
    public class CrossBehaviour : MonoBehaviour
    {
        SwitchablePartBehaviour[] m_lstSwitchableParts;

        public void LookAtStairs()
        {
            if(m_lstSwitchableParts == null)
            {
                m_lstSwitchableParts = FindObjectsByType<SwitchablePartBehaviour>(FindObjectsSortMode.None);
            }

            bool l_bAscendingOrder = MathUtils.HeadsOrTails();

            int l_dStartIndex = l_bAscendingOrder ? 0 : m_lstSwitchableParts.Length - 1;

            int l_dIncrement = l_bAscendingOrder ? 1 : -1;

            bool l_bFoundStairs = false;

            Transform l_trfStairs = null;

            while (!l_bFoundStairs)
            {
                l_bFoundStairs = m_lstSwitchableParts[l_dStartIndex].Part == Enums.ESwitchablePart.STAIRS;

                if(l_bFoundStairs)
                {
                    l_trfStairs = m_lstSwitchableParts[l_dStartIndex].transform;
                }

                l_dStartIndex += l_dIncrement;
            }

            transform.LookAt(l_trfStairs);
        }

        private void Update()
        {

        }
    }
}
