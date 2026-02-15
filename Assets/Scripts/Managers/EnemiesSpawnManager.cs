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

      private List<SpawnPointBehaviour> m_lstAvailableSpawners;

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
         m_lstAvailableSpawners = new List<SpawnPointBehaviour>();
      }

      public void Init(List<int> p_lstEnabledTowerIds, List<ScriptableEnemyWave> p_lstWaves)
      {
         m_lstAvailableSpawners.Clear();

         SpawnPointBehaviour[] l_lstAllSpawnPoints = Object.FindObjectsByType<SpawnPointBehaviour>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

         foreach (SpawnPointBehaviour l_spawnPoint in l_lstAllSpawnPoints)
         {
            TowerBehaviour l_tower = (TowerBehaviour) l_spawnPoint;

            if ((l_tower != null && p_lstEnabledTowerIds.Contains(l_tower.TowerId))
               || l_tower == null)
            {
               m_lstAvailableSpawners.Add(l_spawnPoint);
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
