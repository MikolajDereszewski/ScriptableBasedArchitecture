using UnityEngine;

namespace ScriptableBasedArchitecture
{
    public static class Debugger
    {
        public static void LogEventRaising(string eventMessage, string objectName)
        {
            string message = string.Concat(eventMessage, " event raised from object ", objectName);
            Log(message);
        }

        public static void LogNullError(string objectMissing, string scriptAccessing)
        {
            LogNullError(objectMissing, "access", scriptAccessing);
        }

        public static void LogNullError(string objectMissing, string action, string scriptAccessing)
        {
            string message = string.Concat("Error trying to ", action, " ", objectMissing, " in ", scriptAccessing, "! The object is not assigned!");
            LogError(message);
        }

        public static void Log(string message)
        {
            GetFormattedMessage(ref message);
            Debug.Log(message);
        }

        public static void LogWarning(string message)
        {
            GetFormattedMessage(ref message);
            Debug.LogWarning(message);
        }

        public static void LogError(string message)
        {
            GetFormattedMessage(ref message);
            Debug.LogError(message);
        }

        private static void GetFormattedMessage(ref string message)
        {
            message = string.Concat("[ScriptableBased Debugger] ", message);
        }
    }
}