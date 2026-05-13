using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace BWW.Managers.Map
{
   public sealed class NavMeshManager
   {
      private static NavMeshManager m_instance;

      private List<NavMeshObstacle> m_lstFlags;

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

         m_lstFlags = new List<NavMeshObstacle>();
      }

      public void BuildSurface()
      {
         m_navMeshSurface.RemoveData();

         m_navMeshSurface.BuildNavMesh();
      }

      public void RaiseFlag(GameObject p_goFlag)
      {
         NavMeshObstacle l_flag = p_goFlag.GetComponent<NavMeshObstacle>();

         if (! m_lstFlags.Contains(l_flag))
         {
            m_lstFlags.Add(l_flag);
         }
      }

      public void HandleFlag(GameObject p_goFlag)
      {
         NavMeshObstacle l_flag = p_goFlag.GetComponent<NavMeshObstacle>();
         if (m_lstFlags.Contains(l_flag))
         {
            l_flag.enabled = true;

            l_flag.carving = true;
            m_lstFlags.Remove(l_flag);
         }
      }

      public void DisableObstacle(GameObject p_goObstacle)
      {
         NavMeshObstacle l_obstacle = p_goObstacle.GetComponent<NavMeshObstacle>();

         l_obstacle.carving = false;

         l_obstacle.enabled = false;
      }
   }
}
