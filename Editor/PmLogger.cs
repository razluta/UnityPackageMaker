using UnityEngine;

namespace UnityPackageMaker.Editor
{
    // Encapsulated logging for easy modifications
    public static class PmLogger
    {
        public static void LogError(string message)
        {
            Debug.LogError(message);
        }
        
        public static void LogWarning(string message)
        {
            Debug.LogWarning(message);
        }
        
        public static void Log(string message)
        {
            Debug.Log(message);
        }
    }
}