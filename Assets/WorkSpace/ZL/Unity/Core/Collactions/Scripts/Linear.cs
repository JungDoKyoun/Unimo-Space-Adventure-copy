using UnityEngine;

using ZL.CS;

namespace ZL.Unity.Collections
{
    public struct Linear
    {
        public float value;

        public float length;

        public LinearMode mode;

        public Linear(float value, float length, LinearMode mode)
        {
            this.value = value;

            this.length = length;

            this.mode = mode;
        }

        public float Interval(float delta, LinearMode mode)
        {
            value += delta;

            switch (mode)
            {
                case LinearMode.Clamp:

                    value = Mathf.Clamp(value, 0f, length);

                    break;

                case LinearMode.Repeat:

                    value = Mathf.Repeat(value, length);

                    break;

                case LinearMode.PingPong:

                    value = Mathf.PingPong(value, length);

                    break;

                case LinearMode.Sin:

                    value = Mathf.Repeat(value, MathFEx.PI2);

                    return (Mathf.Sin(value) + 1f) * 0.5f * length;
            }

            return value;
        }
    }
}