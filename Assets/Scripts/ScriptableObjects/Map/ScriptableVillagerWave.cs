using System.Collections.Generic;
using UnityEngine;

namespace BWW.ScriptableObjects.Map
{
    [CreateAssetMenu(fileName = "ScriptableVillagerWave", menuName = "BWW/ScriptableObjects/ScriptableVillagerWave")]
    public class ScriptableVillagersWave : ScriptableObject
    {
        [SerializeField] private List<VillagerCount> m_lstAllPossibleVillagers;

      public List<VillagerCount> AllPossibleVillagers
      {
         get => m_lstAllPossibleVillagers;
      }
   }
}
