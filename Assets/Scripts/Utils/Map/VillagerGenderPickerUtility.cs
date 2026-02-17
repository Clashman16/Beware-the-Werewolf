using BWW.ScriptableObjects.Characters;
using BWW.Utils.Map;
using UnityEngine;

namespace BWW.Utils.Characters
{
   public class VillagerGenderPickerUtility : SpawnPickingUtility
   {
      private const string m_sDatabasePath = "ScriptableObjects/Characters/EnemyAppearanceDatabase";

      private ScriptableVillagerGenderDatabase m_database;

      private GameObject m_goCurrentGender;

      public GameObject CurrentGender
      {
         get => m_goCurrentGender;
      }

      public VillagerGenderPickerUtility() : base()
      {
         m_database = Resources.Load<ScriptableVillagerGenderDatabase>(m_sDatabasePath);

         for(int l_i = 0; l_i < m_database.Genders.Count; l_i++)
         {
            PossiblePicks.Add(l_i);
         }
      }

      public override int Pick()
      {
         int l_dRandomIndex = base.Pick();

         int l_dAppearanceId = PossiblePicks[l_dRandomIndex];

         m_goCurrentGender = m_database.Genders[l_dAppearanceId];

         return l_dAppearanceId;
      }
   }
}
