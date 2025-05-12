using System;

using UnityEngine;

namespace ZL.Unity
{
    public static partial class Mathv
    {
        public static Vector2 Round(this Vector2 v, int digits)
        {
            return new Vector2()
            {
                x = MathF.Round(v.x, digits),

                y = MathF.Round(v.y, digits)
            };
        }

        public static Vector3 Round(this Vector3 v, int digits)
        {
            return new Vector3()
            {
                x = MathF.Round(v.x, digits),

                y = MathF.Round(v.y, digits),

                z = MathF.Round(v.z, digits)
            };
        }
    }
}