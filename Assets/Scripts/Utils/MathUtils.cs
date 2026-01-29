using UnityEngine;

namespace BWW.Utils
{
    public static class MathUtils
    {
        public static bool HeadsOrTails()
        {
            return Random.Range(0, 2) == 1;
        }
    }
}
