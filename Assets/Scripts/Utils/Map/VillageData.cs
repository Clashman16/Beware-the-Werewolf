using BWW.Enums;
using System.Collections.Generic;

namespace BWW.Utils.Map
{
   public struct VillageData
   {
      private Dictionary<bool, int> m_lstAreSkinColorBlackCount;

      public Dictionary<bool, int> AreSkinColorBlackCount
      {
         get => m_lstAreSkinColorBlackCount;
      }

      private Dictionary<bool, int> m_lstAreVillagerWomenCount;

      public Dictionary<bool, int> AreVillagerWomenCount
      {
         get => m_lstAreVillagerWomenCount;
      }

      private Dictionary<EVillagerTitle, int> m_lstVillagerTitleCount;

      public Dictionary<EVillagerTitle, int> VillagerTitleCount
      {
         get => m_lstVillagerTitleCount;
      }

      public VillageData(int[] p_lstBaseCounts)
      {
         m_lstAreSkinColorBlackCount = new Dictionary<bool, int>();

         m_lstAreVillagerWomenCount = new Dictionary<bool, int>();

         m_lstVillagerTitleCount = new Dictionary<EVillagerTitle, int>();

         for(int l_i = 0; l_i < 3; l_i++)
         {
            if(l_i < 2)
            {
               m_lstAreSkinColorBlackCount.Add(l_i == 0, p_lstBaseCounts[0]);

               m_lstAreVillagerWomenCount.Add(l_i == 0, p_lstBaseCounts[1]);
            }

            m_lstVillagerTitleCount.Add((EVillagerTitle)l_i, p_lstBaseCounts[2]);
         }
      }
   }
}
