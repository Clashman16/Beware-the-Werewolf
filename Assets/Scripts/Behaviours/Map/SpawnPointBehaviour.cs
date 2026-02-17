using BWW.Enums;
using BWW.Utils.Characters;
using UnityEngine;

namespace BWW.Behaviours.Map
{
   public class SpawnPointBehaviour : MonoBehaviour
   {
      [SerializeField] private int m_dSpawnerId;

      public int SpawnerId
      {
         get => m_dSpawnerId;
      }

      VillagerGenderPickerUtility m_villagerGenderPicker;

      public VillagerGenderPickerUtility EnemyAppearancePicker
      {
         set => m_villagerGenderPicker = value;
      }

      public void InstantiateVillager(EVillagerType p_eEnemyType)
      {
         m_villagerGenderPicker.Pick();

         Instantiate(m_villagerGenderPicker.CurrentGender);
      }
   }
}
