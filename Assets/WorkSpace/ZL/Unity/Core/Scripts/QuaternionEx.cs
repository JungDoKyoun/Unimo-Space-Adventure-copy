using UnityEngine;

using UnityEngine.Animations;

namespace ZL.Unity
{
    public static partial class QuaternionEx
    {
        public static bool TryLookRotation(Vector3 from, Vector3 to, Axis ignoreAxes, out Quaternion result)
        {
            return TryLookRotation(from, to, Vector3.up, ignoreAxes, out result);
        }

        public static bool TryLookRotation(Vector3 from, Vector3 to, Vector3 upwards, Axis ignoreAxes, out Quaternion result)
        {
            var forward = from.DirectionTo(to, ignoreAxes);

            if (forward.sqrMagnitude == 0f)
            {
                result = Quaternion.identity;

                return false;
            }

            result = Quaternion.LookRotation(forward, upwards);

            return true;
        }
    }
}