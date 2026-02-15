using BWW.Enums;
using BWW.ScriptableObjects.Map;
using System.Collections.Generic;

namespace BWW.Utils.Map
{
   public class EnemyPickerUtility : SpawnPickingUtility
   {
      private List<ScriptableEnemyWave> m_lstWaves;

      private int m_dWaveId;

      private Dictionary<EEnemyType, int> m_lstRemainingEnemies;

      public EnemyPickerUtility(List<ScriptableEnemyWave> p_lstWaves) : base()
      {
         m_lstWaves = p_lstWaves;

         m_dWaveId = 0;

         m_lstRemainingEnemies = new Dictionary<EEnemyType, int>();

         PrepareNextWave();
      }

      private void PrepareNextWave()
      {
         ScriptableEnemyWave l_nextWave = m_lstWaves[m_dWaveId];

         foreach (EnemyCount enemyQty in l_nextWave.AllPossibleEnemies)
         {
            m_lstRemainingEnemies.Add(enemyQty.Enemy, enemyQty.Count);

            PossiblePicks.Add((int) enemyQty.Enemy);
         }

         LastPickedId = -1;
      }

      public override int Pick()
      {
         int l_dRandomEnemyId = base.Pick();

         EEnemyType l_ePickedEnemy = (EEnemyType) PossiblePicks[l_dRandomEnemyId];

         m_lstRemainingEnemies[l_ePickedEnemy] -= 1;

         if(m_lstRemainingEnemies[l_ePickedEnemy] == 0)
         {
            LastPickedId = -1;

            m_lstRemainingEnemies.Remove(l_ePickedEnemy);

            PossiblePicks.Remove((int) l_ePickedEnemy);

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
            LastPickedId = l_dRandomEnemyId;
         }

         return (int) l_ePickedEnemy;
      }
   }
}
