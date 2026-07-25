using BWW.Behaviours.Player;
using BWW.Player;
using UnityEngine;

namespace BWW.Managers.Player
{
   public sealed class PlayerCameraManager
   {
      private static PlayerCameraManager m_instance;

      public static PlayerCameraManager Instance
      {
         get
         {
            if(m_instance == null)
            {
               m_instance = new PlayerCameraManager();
            }

            return m_instance;
         }
      }

      private CameraBehaviour m_camera;

      public CameraBehaviour BWWCamera
      {
         get => m_camera;
      }

      private PlayerCameraManager()
      {
         CameraBehaviour l_camera = Camera.main.GetComponent<CameraBehaviour>();

#if UNITY_WEBGL || UNITY_EDITOR
         if (Application.isMobilePlatform)
         {
            Debug.Log("android navigator");
         }
         else
         {
            l_camera.State = new ComputerCameraState();
         }
#elif UNITY_ANDROID || UNITY_IOS
         Debug.Log("android app");
#endif

         m_camera = l_camera;
      }
   }
}
