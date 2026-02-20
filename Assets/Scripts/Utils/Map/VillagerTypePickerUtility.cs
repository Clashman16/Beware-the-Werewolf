using BWW.Enums;
using BWW.ScriptableObjects.Map;
using System.Collections.Generic;

namespace BWW.Utils.Map
{
   public class VillagerTypePickerUtility : SpawnPickingUtility
   {
      private List<ScriptableVillagersWave> m_lstWaves;

      private int m_dWaveId;

      private Dictionary<EVillagerType, int> m_lstRemainingVillagers;

      public VillagerTypePickerUtility(List<ScriptableVillagersWave> p_lstWaves) : base()
      {
         m_lstWaves = p_lstWaves;

         m_dWaveId = 0;

         m_lstRemainingVillagers = new Dictionary<EVillagerType, int>();

         PrepareNextWave();
      }

      private void PrepareNextWave()
      {
         ScriptableVillagersWave l_nextWave = m_lstWaves[m_dWaveId];

         foreach (VillagerCount villagerQty in l_nextWave.AllPossibleVillagers)
         {
            m_lstRemainingVillagers.Add(villagerQty.Villager, villagerQty.Count);

            PossiblePicks.Add((int) villagerQty.Villager);
         }

         LastPickedId = -1;
      }

      public override int Pick()
      {
         int l_dRandomVillagerId = base.Pick();

         EVillagerType l_ePickedVillager = (EVillagerType) PossiblePicks[l_dRandomVillagerId];

         m_lstRemainingVillagers[l_ePickedVillager] -= 1;

         if(m_lstRemainingVillagers[l_ePickedVillager] == 0)
         {
            LastPickedId = -1;

            m_lstRemainingVillagers.Remove(l_ePickedVillager);

            PossiblePicks.Remove((int) l_ePickedVillager);

            if (m_lstRemainingVillagers.Count == 0)
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
            LastPickedId = l_dRandomVillagerId;
         }

         return (int) l_ePickedVillager;
      }
   }
}
