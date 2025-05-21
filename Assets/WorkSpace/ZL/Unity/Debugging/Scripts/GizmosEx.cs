using UnityEngine;

using ZL.CS;

namespace ZL.Unity.Debugging
{
    public static class GizmosEx
    {
        public static void DrawPolygon(Vector3 center, float radius, int angleCount)
        {
            var prevPoint = center + new Vector3(radius, 0f, 0f);

            for (int i = 1; i <= angleCount; ++i)
            {
                float angle = MathFEx.PI2 * i / angleCount;

                float x = Mathf.Cos(angle) * radius;

                float z = Mathf.Sin(angle) * radius;

                var nextPoint = center + new Vector3(x, 0f, z);

                Gizmos.DrawLine(prevPoint, nextPoint);

                prevPoint = nextPoint;
            }
        }
    }
}