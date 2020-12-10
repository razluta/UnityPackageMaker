using UnityEngine;

namespace UnityPackageMaker.Editor
{
    public static class PmLogger
    {
        // Encapsulated logging for easy modifications
        public static void Log(string message)
        {
            Debug.Log(message);
        }
    }
}