using UnityEngine;

using ZL.CS;

namespace ZL.Unity
{
    public static partial class LayerExtensions
    {
        public static bool Contains(this LayerMask instance, int flags)
        {
            return instance.value.Contains(flags);
        }
    }
}