using System;

using UnityEngine;

using UnityEngine.Animations;

using ZL.CS;

namespace ZL.Unity
{
    public static partial class QuaternionEx
    {
        public static Quaternion LookRotation(Vector3 forward, Axis freezeRotation)
        {
            return LookRotation(forward, Vector3.up, freezeRotation);
        }

        public static Quaternion LookRotation(Vector3 forward, Vector3 upwards, Axis freezeRotation)
        {
            if (freezeRotation.Contains(Axis.X) == true)
            {
                forward.y = 0f;
            }

            if (freezeRotation.Contains(Axis.Y) == true)
            {
                forward.x = 0f;
            }

            if (freezeRotation.Contains(Axis.Z) == true)
            {
                forward.z = 0f;
            }

            if (forward.sqrMagnitude == 0f)
            {
                return Quaternion.identity;
            }

            return Quaternion.LookRotation(forward, upwards);
        }
    }
}