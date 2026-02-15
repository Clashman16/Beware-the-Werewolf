using BWW.Behaviours.Map;
using BWW.Enums;
using BWW.ScriptableObjects.Map;
using BWW.Utils.Map;
using System.Collections.Generic;
using UnityEngine;

namespace BWW.Managers.Map
{
   public sealed class EnemiesSpawnManager
   {
      private static EnemiesSpawnManager m_instance;

      public static EnemiesSpawnManager Instance
      {
         get
         {
            if(m_instance == null)
            {
               m_instance = new EnemiesSpawnManager();
            }

            return m_instance;
         }
      }

      private List<TowerBehaviour> m_lstAvailableSpawners;

      EnemyPickerUtility m_enemyPicker;

      private bool m_bIsReady;

      public bool IsReady
      {
         get => m_bIsReady;
         set
         {
            m_bIsReady = value;

            if(m_bIsReady)
            {
               Spawn();
            }
         }
      }

      private EnemiesSpawnManager()
      {
         m_lstAvailableSpawners = new List<TowerBehaviour>();
      }

      public void Init(List<int> p_lstEnabledTowerIds, List<ScriptableEnemyWave> p_lstWaves)
      {
         m_lstAvailableSpawners.Clear();

         TowerBehaviour[] l_lstAllTowers = Object.FindObjectsByType<TowerBehaviour>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

         foreach (TowerBehaviour l_tower in l_lstAllTowers)
         {
            if (p_lstEnabledTowerIds.Contains(l_tower.TowerId))
            {
               m_lstAvailableSpawners.Add(l_tower);
            }
         }

         m_enemyPicker = new EnemyPickerUtility(p_lstWaves);

         m_bIsReady = false;
      }
      

      public void Spawn()
      {
         EEnemyType l_eEnemy = (EEnemyType) m_enemyPicker.Pick();
      }
   }
}
