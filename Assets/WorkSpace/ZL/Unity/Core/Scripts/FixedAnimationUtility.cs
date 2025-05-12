using System.Diagnostics;

#if UNITY_EDITOR

using UnityEditor;

#endif

using UnityEngine;

namespace ZL.Unity
{
    public static partial class FixedAnimationUtility
    {
        [Conditional("UNITY_EDITOR")]

        public static void SetKeyRightTangentMode(AnimationCurve curve, int index, FixedTangentMode tangentMode)
        {
            #if UNITY_EDITOR

            AnimationUtility.SetKeyRightTangentMode(curve, index, (AnimationUtility.TangentMode)tangentMode);

            #endif
        }

        [Conditional("UNITY_EDITOR")]

        public static void SetKeyLeftTangentMode(AnimationCurve curve, int index, FixedTangentMode tangentMode)
        {
            #if UNITY_EDITOR

            AnimationUtility.SetKeyLeftTangentMode(curve, index, (AnimationUtility.TangentMode)tangentMode);

            #endif
        }
    }
}