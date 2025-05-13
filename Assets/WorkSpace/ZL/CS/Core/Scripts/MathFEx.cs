using System;

namespace ZL.CS
{
    public static partial class MathFEx
    {
        /// <summary>
        /// = PI * 2
        /// </summary>
        public const float PI2 = 6.2831855f;

        /// <summary>
        /// = PI / 2
        /// </summary>
        public const float PIHalf = 1.57079632679f;

        /// <summary>
        /// = SQRT(3)
        /// </summary>
        public const float Root3 = 1.7320508f;

        /// <summary>
        /// = 1 / 60
        /// </summary>
        public const float Normal60 = 0.016666667f;

        /// <summary>
        /// = 1 / 255
        /// </summary>
        public const float Normal255 = 0.003921569f;

        public static float PercentToDecibel(int value)
        {
            return LinearToDecibel(value * 0.01f);
        }

        public static int DecibelToPercent(float value)
        {
            return (int)DecibelToLinear(value) * 100;
        }

        public static float LinearToDecibel(float value)
        {
            if (value <= 0f)
            {
                return -80f;
            }

            return MathF.Log10(value) * 20f;
        }

        public static float DecibelToLinear(float value)
        {
            if (value <= -80f)
            {
                return 0f;
            }

            return MathF.Pow(10f, value * 0.05f);
        }
    }
}