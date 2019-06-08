using UnityEngine;
using UnityEditor;

namespace ScriptableBasedArchitecture
{
    public static class ResetPrefsUtility
    {
        [MenuItem("Scriptables Utilities/Clear Player Prefs")]
        public static void ResetPrefs()
        {
            PlayerPrefs.DeleteAll();
            Debugger.Log("Cleared PlayerPrefs!");
        }
    }
}