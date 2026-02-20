using BWW.ScriptableObjects.Characters;
using System.Collections.Generic;
using UnityEngine;

namespace BWW.Utils.Characters
{
   public sealed class VillagerTextureGetter
   {
      private static VillagerTextureGetter m_instance;

      private Dictionary<string, Texture2D> m_lstTextures;

      public static VillagerTextureGetter Instance
      {
         get
         {
            if(m_instance == null)
            {
               m_instance = new VillagerTextureGetter();
            }

            return m_instance;
         }
      }

      private VillagerTextureGetter()
      {
         ScriptableVillagerTextureDatabase l_database = Resources.Load<ScriptableVillagerTextureDatabase>("ScriptableObjects/Characters/VillagerTextureDatabase");

         m_lstTextures = new Dictionary<string, Texture2D>();

         foreach (VillagerTexture l_tex in l_database.Textures)
         {
            m_lstTextures.Add(l_tex.Key, l_tex.Texture);
         }
      }

      public Texture2D GetTextureFromKey(string p_sKey)
      {
         return m_lstTextures[p_sKey];
      }
   }
}
