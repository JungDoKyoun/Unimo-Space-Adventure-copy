// Working

using UnityEngine;

namespace ZL.Unity.Phys
{
    public static partial class PhysicsEx
    {
        public static bool BoxCast(in Vector3 start, in Quaternion rotation, in Vector3 scale, out RaycastHit raycastHit, LayerMask layerMask)
        {
            return Physics.BoxCast(start, scale * 0.5f, Vector3.zero, out raycastHit, rotation, 0f, layerMask);
        }

        public static bool BoxCast(in Vector3 start, in Vector3 end, in Quaternion rotation, in Vector3 scale, out RaycastHit raycastHit, LayerMask layerMask)
        {
            return Physics.BoxCast(start, scale * 0.5f, (end - start).normalized, out raycastHit, rotation, Vector3.Distance(start, end), layerMask);
        }

        public static bool BoxCastAll(in Vector3 start, in Quaternion rotation, in Vector3 scale, out RaycastHit[] raycastHits, LayerMask layerMask)
        {
            raycastHits = Physics.BoxCastAll(start, scale * 0.5f, Vector3.zero, rotation, 0f, layerMask);

            return raycastHits.Length > 0;
        }

        public static bool BoxCastAll(in Vector3 start, in Vector3 end, in Quaternion rotation, in Vector3 scale, out RaycastHit[] raycastHits, LayerMask layerMask)
        {
            raycastHits = Physics.BoxCastAll(start, scale * 0.5f, (end - start).normalized, rotation, Vector3.Distance(start, end), layerMask);

            return raycastHits.Length > 0;
        }

        public static bool LineCast(in Vector3 start, in Vector3 end, float length, out RaycastHit raycastHit, LayerMask layerMask)
        {
            return Physics.Raycast(start, (end - start).normalized, out raycastHit, length, layerMask);
        }

        public static bool LineCastAll(in Vector3 start, in Vector3 end, out RaycastHit[] raycastHits, LayerMask layerMask)
        {
            raycastHits = Physics.RaycastAll(start, end, Vector3.Distance(start, end), layerMask);

            return raycastHits.Length > 0;
        }

        public static bool LineCastAll(in Vector3 start, in Vector3 end, float length, out RaycastHit[] raycastHits, LayerMask layerMask)
        {
            raycastHits = Physics.RaycastAll(start, (end - start).normalized, length, layerMask);

            return raycastHits.Length > 0;
        }

        public static bool SphereCast(in Vector3 start, float radius, out RaycastHit raycastHit, LayerMask layerMask)
        {
            return Physics.SphereCast(start, radius, Vector3.zero, out raycastHit, 0f, layerMask);
        }

        public static bool SphereCast(in Vector3 start, in Vector3 end, float radius, out RaycastHit raycastHit, LayerMask layerMask)
        {
            return Physics.SphereCast(start, radius, (end - start).normalized, out raycastHit, Vector3.Distance(start, end), layerMask);
        }

        public static bool SphereCastAll(in Vector3 start, float radius, out RaycastHit[] raycastHits, LayerMask layerMask)
        {
            raycastHits = Physics.SphereCastAll(start, radius, Vector3.zero, 0f, layerMask);

            return raycastHits.Length > 0;
        }

        public static bool SphereCastAll(in Vector3 start, in Vector3 end, float radius, out RaycastHit[] raycastHits, LayerMask layerMask)
        {
            raycastHits = Physics.SphereCastAll(start, radius, (end - start).normalized, Vector3.Distance(start, end), layerMask);

            return raycastHits.Length > 0;
        }
    }
}