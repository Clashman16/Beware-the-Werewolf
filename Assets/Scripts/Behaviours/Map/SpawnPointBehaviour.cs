using BWW.Enums;
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

      public void InstantiateEnemy(EEnemyType p_eEnemyType)
      {

      }
   }
}
