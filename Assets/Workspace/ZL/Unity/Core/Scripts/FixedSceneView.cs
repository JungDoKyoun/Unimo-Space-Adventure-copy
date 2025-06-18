using System.Diagnostics;

#if UNITY_EDITOR

using UnityEditor;

#endif

namespace ZL.Unity
{
    public static class FixedSceneView
    {
        [Conditional("UNITY_EDITOR")]

        public static void RepaintAll()
        {
            #if UNITY_EDITOR

            SceneView.RepaintAll();

            #endif
        }
    }
}