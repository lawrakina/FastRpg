using UnityEngine;


namespace Helper
{
    public static class Dbg
    {
        public static bool LogingEnabled = true;

        public static void Log(string stringToLog)
        {
            Debug.Log(stringToLog);
        }

        public static void DebugWireSphere(Vector3 position, Color color, float distance)
        {
            DebugExtension.DebugWireSphere(position, color, distance);
        }

        public static void DrawRay(Vector3 transformPosition, Vector3 hitDistance, Color color)
        {
            Debug.DrawRay(transformPosition, hitDistance, color);
        }
    }
}