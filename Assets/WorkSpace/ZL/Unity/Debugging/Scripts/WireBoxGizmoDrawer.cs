using UnityEngine;

namespace ZL.Unity.Debugging
{
    public sealed class WirwBoxGizmoDrawer : BoxGizmoDrawer
    {
        protected override void DrawGizmo()
        {
            Gizmos.DrawWireCube(center, size);
        }
    }
}