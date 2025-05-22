using UnityEngine;

namespace ZL.Unity.Debugging
{
    public abstract class GizmoDrawer : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private bool drawOnSelected = true;

        [Space]

        [SerializeField]

        private Color color = new Color(1f, 0f, 0f, 0.5f);

        [SerializeField]

        protected Vector3 center = Vector3.zero;

        private void OnDrawGizmos()
        {
            if (drawOnSelected == true)
            {
                return;
            }

            SetupGizmo();

            DrawGizmo();
        }

        private void OnDrawGizmosSelected()
        {
            if (drawOnSelected == false)
            {
                return;
            }

            SetupGizmo();

            DrawGizmo();
        }

        protected virtual void SetupGizmo()
        {
            Gizmos.color = color;

            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        }

        protected abstract void DrawGizmo();
    }
}