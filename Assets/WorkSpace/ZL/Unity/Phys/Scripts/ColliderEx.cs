using UnityEngine;

namespace ZL.Unity.Phys
{
    public static class ColliderEx
    {
        public static Vector3 ClosestPoint(this Collider instance, Collider other)
        {
            return instance.ClosestPoint(other.bounds.center);
        }
    }
}