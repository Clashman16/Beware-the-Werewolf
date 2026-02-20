using System;
using UnityEngine;

namespace BWW.ScriptableObjects.Characters
{
   [Serializable]
   public struct VillagerTexture
   {
      [SerializeField] private string m_sTextureKey;

      public string Key
      {
         get => m_sTextureKey;
      }

      [SerializeField] private Texture2D m_texture;

      public Texture2D Texture
      {
         get => m_texture;
      }
   }
}
