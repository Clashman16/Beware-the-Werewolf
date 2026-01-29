using BWW.Enums;
using BWW.ScriptableObjects.Map;
using System.Collections.Generic;
using UnityEngine;

namespace BWW.Behaviours.Map
{
   public class SwitchablePartBehaviour : MonoBehaviour
   {
      [SerializeField] private List<int> m_lstNeigborSpawners;

      [SerializeField] private List<SwitchablePartCount> m_lstPartIds;

      private ESwitchablePart m_ePart;

      private Dictionary<ESwitchablePart, GameObject> m_lstPossibleParts;

      private void Awake()
      {
         m_lstPossibleParts = new Dictionary<ESwitchablePart, GameObject>();

         for (int l_i = 0; l_i < m_lstPartIds.Count; l_i++)
         {
            m_lstPossibleParts.Add(m_lstPartIds[l_i].Part, transform.GetChild(m_lstPartIds[l_i].Count).gameObject);
         }

         m_ePart = ESwitchablePart.WALL;
      }

      public void Switch(ESwitchablePart partType)
      {
         m_lstPossibleParts[m_ePart].SetActive(false);

         m_ePart = partType;

         m_lstPossibleParts[m_ePart].SetActive(true);
      }

      public bool IsCloseToSpawner(List<int> p_lstSpawnersId)
      {
         for (int l_i = 0; l_i < p_lstSpawnersId.Count; l_i++)
         {
            if (m_lstNeigborSpawners.Contains(p_lstSpawnersId[l_i]))
            {
               return true;
            }
         }

         return false;
      }
   }
}

