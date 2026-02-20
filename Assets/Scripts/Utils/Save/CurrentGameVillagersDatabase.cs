using BWW.Behaviours.Characters;
using System.Collections.Generic;

namespace BWW.Utils.Save
{
   public sealed class CurrentGameVillagersDatabase
   {
      private static CurrentGameVillagersDatabase m_instance;

      public static CurrentGameVillagersDatabase Instance
      {
         get
         {
            if (m_instance == null)
            {
               m_instance = new CurrentGameVillagersDatabase();
            }

            return m_instance;
         }
      }

      private VillageData m_villageData;

      public VillageData Village
      {
         get => m_villageData;
      }

      private List<VillagerAppearanceBehaviour> m_lstGraveyard;

      public List<VillagerAppearanceBehaviour> Graveyard
      {
         get => m_lstGraveyard;
      }

      private CurrentGameVillagersDatabase()
      {
         m_villageData = new VillageData(new int[] {0, 0, 0});

         m_lstGraveyard = new List<VillagerAppearanceBehaviour>();
      }
   }
}
