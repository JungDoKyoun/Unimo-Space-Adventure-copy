using UnityEngine;

namespace ZL.Unity
{
    public static partial class MathfEx
    {
        public static int RoundRandom(float f)
        {
            int integerPart = (int)f;

            float decimalPart = f - integerPart;

            if (decimalPart > 0f)
            {
                integerPart += Random.Range(0f, 1f) < decimalPart ? 1 : 0;
            }

            return integerPart;
        }
    }
}