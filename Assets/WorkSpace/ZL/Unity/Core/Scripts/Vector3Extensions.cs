using UnityEngine;

namespace ZL.Unity
{
    public static partial class Vector3Extensions
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
    }
}