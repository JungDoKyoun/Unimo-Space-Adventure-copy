using UnityEngine;

namespace ZL.Unity
{
    public static partial class LayerExtensions
    {
        public static bool Contains(this LayerMask instance, int layer)
        {
            return (instance.value & (1 << layer)) != 0;
        }
    }
}