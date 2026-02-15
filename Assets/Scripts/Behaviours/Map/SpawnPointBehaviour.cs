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

      EnemyAppearancePickerUtility m_enemyAppearancePicker;

      public EnemyAppearancePickerUtility EnemyAppearancePicker
      {
         set => m_enemyAppearancePicker = value;
      }

      public void InstantiateEnemy(EEnemyType p_eEnemyType)
      {
         m_enemyAppearancePicker.Pick();

         Instantiate(m_enemyAppearancePicker.CurrentAppearance);
      }
   }
}
