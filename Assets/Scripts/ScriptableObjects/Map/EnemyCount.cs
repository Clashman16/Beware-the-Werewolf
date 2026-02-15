using BWW.Enums;
using System;
using UnityEngine;

namespace BWW.ScriptableObjects.Map
{
   [Serializable]
   public struct EnemyCount
   {
      [SerializeField] private EEnemyType m_eEnemy;

      public EEnemyType Enemy
      {
         get => m_eEnemy;
      }

      [SerializeField] private int m_dCount;

      public int Count
      {
         get => m_dCount;
      }
   }
}
