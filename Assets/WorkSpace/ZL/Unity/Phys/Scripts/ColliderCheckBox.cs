using UnityEngine;

namespace ZL.Unity.Phys
{
    [AddComponentMenu("ZL/Phys/Collider Check Box")]

    public sealed class ColliderCheckBox : ColliderChecker
    {
        #if UNITY_EDITOR

        protected override void DrawGizmos()
        {
            if (isWireGizmo == true)
            {
                Gizmos.DrawWireCube(Vector3.zero, transform.lossyScale);
            }

            else
            {
                Gizmos.DrawCube(Vector3.zero, transform.lossyScale);
            }
        }

        #endif

        public override bool Check()
        {
            return Physics.CheckBox(transform.position, transform.lossyScale * 0.5f, transform.rotation, layerMask, triggerInteraction);
        }
    }
}