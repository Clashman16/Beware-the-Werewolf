using BWW.Enums;
using BWW.ScriptableObjects.Map;
using System.Collections.Generic;
using UnityEngine;

namespace BWW.Utils.Map
{
   public class EnemyPickerUtility
   {
      private List<ScriptableEnemyWave> m_lstWaves;

      private int m_dWaveId;

      private Dictionary<EEnemyType, int> m_lstRemainingEnemies;

      private int m_dLastEnemyId;

      private List<EEnemyType> m_lstPossibleEnemies;

      public EnemyPickerUtility(List<ScriptableEnemyWave> p_lstWaves)
      {
         m_lstWaves = p_lstWaves;

         m_dWaveId = 0;

         m_lstRemainingEnemies = new Dictionary<EEnemyType, int>();

         m_lstPossibleEnemies = new List<EEnemyType>();

         PrepareNextWave();
      }

      private void PrepareNextWave()
      {
         ScriptableEnemyWave l_nextWave = m_lstWaves[m_dWaveId];

         foreach (EnemyCount enemyQty in l_nextWave.AllPossibleEnemies)
         {
            m_lstRemainingEnemies.Add(enemyQty.Enemy, enemyQty.Count);

            m_lstPossibleEnemies.Add(enemyQty.Enemy);
         }

         m_dLastEnemyId = -1;
      }

      private int GetRandomEnemyId()
      {
         return Random.Range(0, m_lstPossibleEnemies.Count);
      }

      public int Pick()
      {
         if(m_dLastEnemyId == -1)
         {
            m_dLastEnemyId = GetRandomEnemyId();
         }

         int l_dRandomEnemyId = GetRandomEnemyId();

         if(m_dLastEnemyId == l_dRandomEnemyId)
         {
            if(MathUtils.HeadsOrTails())
            {
               l_dRandomEnemyId = GetRandomEnemyId();
            }
         }

         EEnemyType l_ePickedEnemy = m_lstPossibleEnemies[l_dRandomEnemyId];

         m_lstRemainingEnemies[l_ePickedEnemy] -= 1;

         if(m_lstRemainingEnemies[l_ePickedEnemy] == 0)
         {
            m_dLastEnemyId = -1;

            m_lstRemainingEnemies.Remove(l_ePickedEnemy);

            m_lstPossibleEnemies.Remove(l_ePickedEnemy);

            if (m_lstRemainingEnemies.Count == 0)
            {
               m_dWaveId += 1;

               if (m_dWaveId == m_lstWaves.Count)
               {
                  m_dWaveId = 0;
               }

               PrepareNextWave();
            }
         }
         else
         {
            m_dLastEnemyId = l_dRandomEnemyId;
         }

         return (int) l_ePickedEnemy;
      }
   }
}
