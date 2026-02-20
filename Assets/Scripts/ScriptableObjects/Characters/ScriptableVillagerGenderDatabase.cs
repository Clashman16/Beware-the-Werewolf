using System.Collections.Generic;
using UnityEngine;

namespace BWW.ScriptableObjects.Characters
{
   [CreateAssetMenu(fileName = "ScriptableVillagerGenderDatabase", menuName = "BWW/ScriptableObjects/ScriptableVillagerGenderDatabase")]
   public class ScriptableVillagerGenderDatabase : ScriptableObject
   {
      [SerializeField] private List<GameObject> m_lstGenders;

      public List<GameObject> Genders
      {
         get => m_lstGenders;
      }
   }
}
