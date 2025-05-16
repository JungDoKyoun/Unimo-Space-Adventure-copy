using UnityEngine;

namespace ZL.Unity.Debugging
{
    public class SphereGizmoDrawer : GizmoDrawer
    {
        [SerializeField]

        protected float radius = 0.5f;

        protected override void DrawGizmo()
        {
            Gizmos.DrawSphere(center, radius);
        }
    }
}