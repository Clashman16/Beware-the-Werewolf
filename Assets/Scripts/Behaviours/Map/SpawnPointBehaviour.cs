using BWW.Behaviours.Characters;
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
         bool l_bIsCharacterFemale = m_villagerGenderPicker.Pick() == 1;

         GameObject l_goVillager = Instantiate(m_villagerGenderPicker.CurrentGender);

         VillagerAppearanceBehaviour l_villager = l_goVillager.GetComponent<VillagerAppearanceBehaviour>();

         l_villager.UpdateAppearance(l_bIsCharacterFemale);
      }
   }
}
