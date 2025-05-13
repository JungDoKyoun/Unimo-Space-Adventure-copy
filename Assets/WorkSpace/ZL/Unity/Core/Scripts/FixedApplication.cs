#if UNITY_EDITOR

using UnityEditor;

#endif

using UnityEngine;

namespace ZL.Unity
{
    public static partial class FixedApplication
    {
        public static void Quit()
        {
            #if UNITY_EDITOR

            EditorApplication.isPlaying = false;

            #endif

            Application.Quit();
        }
    }
}