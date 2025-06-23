using UnityEngine;

using UnityEngine.Animations;

using ZL.CS;

namespace ZL.Unity
{
    public static partial class Vector3Ex
    {
        public static Color ToColor(this Vector3 instance)
        {
            instance = Absoulute(instance);

            float max = GetMaxAxis(instance);

            if (max > 1f)
            {
                instance *= 1f / max;
            }

            return new Color(instance.x, instance.y, instance.z);
        }

        public static Vector3 Absoulute(this Vector3 instance)
        {
            if (instance.x < 0)
            {
                instance.x = -instance.x;
            }

            if (instance.y < 0)
            {
                instance.y = -instance.y;
            }

            if (instance.z < 0)
            {
                instance.z = -instance.z;
            }

            return instance;
        }

        public static float GetMinAxis(this Vector3 instance)
        {
            float min = instance.x;

            if (min > instance.y)
            {
                min = instance.y;
            }

            if (min > instance.z)
            {
                min = instance.z;
            }

            return min;
        }

        public static float GetMaxAxis(this Vector3 instance)
        {
            float max = instance.x;

            if (max < instance.y)
            {
                max = instance.y;
            }

            if (max < instance.z)
            {
                max = instance.z;
            }

            return max;
        }

        public static float DistanceTo(this Vector3 instance, Vector3 to, Axis ignoreAxes)
        {
            return DirectionTo(instance, to, ignoreAxes).magnitude;
        }

        public static Vector3 DirectionTo(this Vector3 instance, Vector3 to, Axis ignoreAxes)
        {
            var direction = to - instance;

            if (EnumEx.HasFlag(ignoreAxes, Axis.X) == true)
            {
                direction.x = 0f;
            }

            if (EnumEx.HasFlag(ignoreAxes, Axis.Y) == true)
            {
                direction.y = 0f;
            }

            if (EnumEx.HasFlag(ignoreAxes, Axis.Z) == true)
            {
                direction.z = 0f;
            }

            return direction;
        }
    }
}