using System.Diagnostics;

#if UNITY_EDITOR

using UnityEditor;

#endif

using UnityEngine;

namespace ZL.Unity
{
    public static partial class FixedSelection
    {
        [Conditional("UNITY_EDITOR")]

        public static void SetActiveObject(Object value)
        {
            #if UNITY_EDITOR

            Selection.activeObject = value;

            #endif
        }
    }
}