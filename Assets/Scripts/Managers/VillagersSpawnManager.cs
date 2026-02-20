using BWW.Enums;
using BWW.ScriptableObjects.Map;
using BWW.Utils.Characters;
using BWW.Utils.Map;
using System.Collections.Generic;

namespace BWW.Managers.Map
{
   public sealed class VillagersSpawnManager
   {
      private static VillagersSpawnManager m_instance;

      public static VillagersSpawnManager Instance
      {
         get
         {
            if(m_instance == null)
            {
               m_instance = new VillagersSpawnManager();
            }

            return m_instance;
         }
      }

      VillagerTypePickerUtility m_villagerTypePicker;

      VillagerGenderPickerUtility m_villagerGenderPicker;

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

      private VillagersSpawnManager()
      {
         m_villagerGenderPicker = new VillagerGenderPickerUtility();
      }

      public void Init(List<int> p_lstEnabledTowerIds, List<ScriptableVillagersWave> p_lstWaves)
      {
         m_spawnerPicker = new SpawnPointPickerUtility(p_lstEnabledTowerIds, m_villagerGenderPicker);

         m_villagerTypePicker = new VillagerTypePickerUtility(p_lstWaves);

         m_bIsReady = false;
      }
      

      public void Spawn()
      {
         EVillagerType l_eVillagerType = (EVillagerType) m_villagerTypePicker.Pick();

         m_spawnerPicker.Pick();

         m_spawnerPicker.CurrentSpawnPoint.InstantiateVillager(l_eVillagerType);
      }
   }
}
