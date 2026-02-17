using BWW.Enums;
using System;
using UnityEngine;

namespace BWW.ScriptableObjects.Map
{
   [Serializable]
   public struct VillagerCount
   {
      [SerializeField] private EVillagerType m_eVillager;

      public EVillagerType Villager
      {
         get => m_eVillager;
      }

      [SerializeField] private int m_dCount;

      public int Count
      {
         get => m_dCount;
      }
   }
}
