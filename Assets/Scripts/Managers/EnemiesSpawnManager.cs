using BWW.Enums;
using BWW.ScriptableObjects.Map;
using BWW.Utils.Map;
using System.Collections.Generic;

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

      EnemyPickerUtility m_enemyPicker;

      SpawnPointPickerUtility m_spawnerPicker;

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

      private EnemiesSpawnManager(){}

      public void Init(List<int> p_lstEnabledTowerIds, List<ScriptableEnemyWave> p_lstWaves)
      {
         m_spawnerPicker = new SpawnPointPickerUtility(p_lstEnabledTowerIds);

         m_enemyPicker = new EnemyPickerUtility(p_lstWaves);

         m_bIsReady = false;
      }
      

      public void Spawn()
      {
         EEnemyType l_eEnemy = (EEnemyType) m_enemyPicker.Pick();

         m_spawnerPicker.Pick();

         m_spawnerPicker.CurrentSpawnPoint.InstantiateEnemy(l_eEnemy);
      }
   }
}
