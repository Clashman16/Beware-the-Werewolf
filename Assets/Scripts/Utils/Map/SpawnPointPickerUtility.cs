using BWW.Behaviours.Map;
using BWW.Utils.Characters;
using System.Collections.Generic;
using UnityEngine;

namespace BWW.Utils.Map
{
   public class SpawnPointPickerUtility : SpawnPickingUtility
   {
      private Dictionary<int, SpawnPointBehaviour> m_lstAvailableSpawners;

      private SpawnPointBehaviour m_currentSpawnPoint;

      public SpawnPointBehaviour CurrentSpawnPoint
      {
         get => m_currentSpawnPoint;
      }

      public SpawnPointPickerUtility(List<int> p_lstEnabledTowerIds, VillagerGenderPickerUtility p_enemyAppearancePicker) : base()
      {
         m_lstAvailableSpawners = new Dictionary<int, SpawnPointBehaviour>();

         SpawnPointBehaviour[] l_lstAllSpawnPoints = Object.FindObjectsByType<SpawnPointBehaviour>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

         for(int l_i = 0; l_i < l_lstAllSpawnPoints.Length; l_i++)
         {
            SpawnPointBehaviour l_spawnPoint = l_lstAllSpawnPoints[l_i];

            TowerBehaviour l_tower = (TowerBehaviour) l_spawnPoint;

            if ((l_tower != null && p_lstEnabledTowerIds.Contains(l_tower.SpawnerId))
               || l_tower == null)
            {
               PossiblePicks.Add(l_spawnPoint.SpawnerId);

               m_lstAvailableSpawners.Add(l_spawnPoint.SpawnerId, l_spawnPoint);

               l_spawnPoint.EnemyAppearancePicker = p_enemyAppearancePicker;
            }
         }
      }

      public override int Pick()
      {
         int l_dRandomIndex = base.Pick();

         int l_dPickingId = PossiblePicks[l_dRandomIndex];

         m_currentSpawnPoint = m_lstAvailableSpawners[l_dPickingId];

         return 0;
      }
   }
}
