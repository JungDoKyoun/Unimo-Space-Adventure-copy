using UnityEngine;

namespace ZL.Unity.Debugging
{
    public class BoxGizmoDrawer : GizmoDrawer
    {
        [SerializeField]

        protected Vector3 size = Vector3.one;

        protected override void DrawGizmo()
        {
            Gizmos.DrawCube(center, size);
        }
    }
}