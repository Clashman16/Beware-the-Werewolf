using BWW.Managers.Player;
using UnityEngine;

namespace BWW.Utils
{
    public static class MathUtils
    {
        private static Camera m_camera;

        public static bool HeadsOrTails()
        {
            return Random.Range(0, 2) == 1;
        }

        public static Vector2 GetScreenPosition(Vector3 p_vecPositionToConvert)
        {
            if (m_camera == null)
            {
                m_camera = PlayerCameraManager.Instance.BWWCamera.UnityCamera;
            }

            return RectTransformUtility.WorldToScreenPoint(m_camera, p_vecPositionToConvert);
        }
    }
}
