using UnityEngine;

using UnityEngine.Animations;

namespace ZL.Unity
{
    public static partial class QuaternionEx
    {
        public static Quaternion LookRotation(Vector3 from, Vector3 to, Axis ignoreAxes)
        {
            return LookRotation(from, to, Vector3.up, ignoreAxes);
        }

        public static Quaternion LookRotation(Vector3 from, Vector3 to, Vector3 upwards, Axis ignoreAxes)
        {
            var forward = Vector3Ex.Direction(from, to, ignoreAxes);

            return Quaternion.LookRotation(forward, upwards);
        }
    }
}