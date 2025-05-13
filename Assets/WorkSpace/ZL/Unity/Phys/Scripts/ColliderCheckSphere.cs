using UnityEngine;

namespace ZL.Unity.Phys
{
    [AddComponentMenu("ZL/Phys/Collider Check Sphere")]

    public sealed class ColliderCheckSphere : ColliderChecker
    {
        #if UNITY_EDITOR

        protected override void DrawGizmos()
        {
            if (isWireGizmo == true)
            {
                Gizmos.DrawWireSphere(Vector3.zero, transform.lossyScale.GetMaxAxis() * 0.5f);
            }

            else
            {
                Gizmos.DrawSphere(Vector3.zero, transform.lossyScale.GetMaxAxis() * 0.5f);
            }
        }

        #endif

        public override bool Check()
        {
            return Physics.CheckSphere(transform.position, transform.lossyScale.GetMaxAxis() * 0.5f, layerMask, triggerInteraction);
        }
    }
}