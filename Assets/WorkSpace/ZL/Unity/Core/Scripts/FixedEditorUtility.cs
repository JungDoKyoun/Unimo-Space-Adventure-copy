using System.Diagnostics;

#if UNITY_EDITOR

using UnityEditor;

#endif

using UnityEngine;

namespace ZL.Unity
{
    public static partial class FixedEditorUtility
    {
        [Conditional("UNITY_EDITOR")]

        public static void SetDirty(Object target)
        {
            #if UNITY_EDITOR

            EditorUtility.SetDirty(target);

            #endif
        }

        public static bool DisplayDialog(string title, string message, string ok)
        {
            return DisplayDialog(title, message, ok, string.Empty);
        }

        public static bool DisplayDialog(string title, string message, string ok, string cancel)
        {
            #if UNITY_EDITOR

            return EditorUtility.DisplayDialog(title, message, ok, cancel);

            #else

            return false;

            #endif
        }
    }
}