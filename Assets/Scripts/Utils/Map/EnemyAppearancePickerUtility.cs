using BWW.ScriptableObjects.Characters;
using BWW.Utils.Map;
using UnityEngine;

namespace BWW.Utils.Characters
{
   public class EnemyAppearancePickerUtility : SpawnPickingUtility
   {
      private const string m_sDatabasePath = "ScriptableObjects/Characters/EnemyAppearanceDatabase";

      private ScriptableEnemyAppearanceDatabase m_database;

      private GameObject m_goCurrentAppearance;

      public GameObject CurrentAppearance
      {
         get => m_goCurrentAppearance;
      }

      public EnemyAppearancePickerUtility() : base()
      {
         m_database = Resources.Load<ScriptableEnemyAppearanceDatabase>(m_sDatabasePath);

         for(int l_i = 0; l_i < m_database.Appearances.Count; l_i++)
         {
            PossiblePicks.Add(l_i);
         }
      }

      public override int Pick()
      {
         int l_dRandomIndex = base.Pick();

         int l_dAppearanceId = PossiblePicks[l_dRandomIndex];

         m_goCurrentAppearance = m_database.Appearances[l_dAppearanceId];

         return 0;
      }
   }
}
