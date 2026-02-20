using BWW.ScriptableObjects.Characters;
using BWW.Utils.Map;
using BWW.Utils.Save;
using System.Collections.Generic;
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
         int l_dRandomIndex;

         Dictionary<bool, int> l_lstAreVillagerWomenCount = CurrentGameVillagersDatabase.Instance.Village.AreVillagerWomenCount;

         if (l_lstAreVillagerWomenCount[true] == l_lstAreVillagerWomenCount[false])
         {
            l_dRandomIndex = base.Pick();
         }
         else
         {
            l_dRandomIndex = l_lstAreVillagerWomenCount[true] > l_lstAreVillagerWomenCount[false] ? 0 : 1;
         }

         int l_dAppearanceId = PossiblePicks[l_dRandomIndex];

         m_goCurrentGender = m_database.Genders[l_dAppearanceId];

         return l_dAppearanceId;
      }
   }
}
