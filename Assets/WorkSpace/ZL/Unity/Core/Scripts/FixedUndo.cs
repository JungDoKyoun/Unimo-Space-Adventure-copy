using System.Diagnostics;

#if UNITY_EDITOR

using UnityEditor;

#endif

using UnityEngine;

namespace ZL.Unity
{
    public static partial class FixedUndo
    {
        [Conditional("UNITY_EDITOR")]

        public static void RegisterCreatedObjectUndo(Object objectToUndo, string name)
        {
            #if UNITY_EDITOR

            if (EditorApplication.isPlaying == true)
            {
                return;
            }

            Undo.RegisterCreatedObjectUndo(objectToUndo, name);

            #endif
        }

        [Conditional("UNITY_EDITOR")]

        public static void RecordObject(Object objectToUndo, string name)
        {
            #if UNITY_EDITOR

            if (EditorApplication.isPlaying == true)
            {
                return;
            }

            Undo.RecordObject(objectToUndo, name);

            #endif
        }
    }
}