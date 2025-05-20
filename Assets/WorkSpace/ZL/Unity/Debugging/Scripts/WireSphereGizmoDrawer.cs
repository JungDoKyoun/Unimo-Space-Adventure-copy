using UnityEngine;

namespace ZL.Unity.Debugging
{
    public sealed class WireSphereGizmoDrawer : SphereGizmoDrawer
    {
        protected override void DrawGizmo()
        {
            Gizmos.DrawWireSphere(center, radius);
        }
    }
}