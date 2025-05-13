using System.Collections.Generic;

namespace ZL.Unity.Coroutines
{
    public sealed class WaitForSecondsComparer : IEqualityComparer<float>
    {
        bool IEqualityComparer<float>.Equals(float x, float y)
        {
            return x == y;
        }

        int IEqualityComparer<float>.GetHashCode(float obj)
        {
            return obj.GetHashCode();
        }
    }
}