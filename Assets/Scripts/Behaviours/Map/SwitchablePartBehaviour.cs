using BWW.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace BWW.Behaviours.Map
{
    public class SwitchablePartBehaviour : MonoBehaviour
    {
        private ESwitchablePart m_ePart;

        private Dictionary<ESwitchablePart, GameObject> m_lstPossibleParts;

        private void Awake()
        {
            m_lstPossibleParts = new Dictionary<ESwitchablePart, GameObject>();
        }

        private void Start()
        {
            for (int l_i = 0; l_i < transform.childCount; l_i++)
            {
                m_lstPossibleParts.Add((ESwitchablePart) l_i, transform.GetChild(l_i).gameObject);
            }

            int partIndex = Random.Range(0, m_lstPossibleParts.Count);

            Switch(partIndex);
        }

        public void Switch(int partIndex)
        {
            m_lstPossibleParts[m_ePart].SetActive(false);

            m_ePart = (ESwitchablePart) partIndex;

            m_lstPossibleParts[m_ePart].SetActive(true);
        }
    }
}

