using Unity.AI.Navigation;
using UnityEngine;

namespace BWW.Managers.Map
{
   public sealed class NavMeshManager
   {
      private static NavMeshManager m_instance;

      public static NavMeshManager Instance
      {
         get
         {
            if (m_instance == null)
            {
               m_instance = new NavMeshManager();
            }

            return m_instance;
         }
      }

      private NavMeshSurface m_navMeshSurface;

      private NavMeshManager()
      {
         m_navMeshSurface = Object.FindAnyObjectByType<NavMeshSurface>();
      }

      public void BuildSurface()
      {
         m_navMeshSurface.BuildNavMesh();
      }
   }
}
