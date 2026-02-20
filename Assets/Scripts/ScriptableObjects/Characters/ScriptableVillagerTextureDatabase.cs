using System.Collections.Generic;
using UnityEngine;

namespace BWW.ScriptableObjects.Characters
{
   [CreateAssetMenu(fileName = "ScriptableVillagerTextureDatabase", menuName = "BWW/ScriptableObjects/ScriptableVillagerTextureDatabase")]
   public class ScriptableVillagerTextureDatabase : ScriptableObject
   {
      [SerializeField] private List<VillagerTexture> m_lstTextures;

      public List<VillagerTexture> Textures
      {
         get => m_lstTextures;
      }
   }
}
