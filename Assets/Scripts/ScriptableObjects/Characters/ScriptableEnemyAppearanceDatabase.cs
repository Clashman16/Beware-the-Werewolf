using System.Collections.Generic;
using UnityEngine;

namespace BWW.ScriptableObjects.Characters
{
   [CreateAssetMenu(fileName = "ScriptableEnemyAppearanceDatabase", menuName = "BWW/ScriptableObjects/ScriptableEnemyAppearanceDatabase")]
   public class ScriptableEnemyAppearanceDatabase : ScriptableObject
   {
      [SerializeField] private List<GameObject> m_lstAppearances;

      public List<GameObject> Appearances
      {
         get => m_lstAppearances;
      }
   }
}
