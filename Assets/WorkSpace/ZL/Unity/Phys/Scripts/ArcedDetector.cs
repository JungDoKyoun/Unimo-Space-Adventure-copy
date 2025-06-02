#if UNITY_EDITOR

using UnityEditor;

#endif

using UnityEngine;

namespace ZL.Unity.Phys
{
    [AddComponentMenu("ZL/Phys/Arced Detector")]

    public sealed class ArcedDetector : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private float radius = 3f;

        [Space]

        [SerializeField]

        private float angle = 60f;

        public float Angle
        {
            get => angle;

            set => angle = value;
        }

#if UNITY_EDITOR

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Line(Margin = 0)]

        [Text("<b>Debugging</b>", FontSize = 16)]

        [Margin]

        private bool drawGizmo = true;

        [SerializeField]

        [UsingCustomProperty]

        [ToggleIf(nameof(drawGizmo), false)]

        [AddIndent(1)]

        [Alias("Is Wire")]

        private bool isWireGizmo = false;

        [SerializeField]

        [UsingCustomProperty]

        [ToggleIf(nameof(drawGizmo), false)]

        [AddIndent(1)]

        [Alias("Default Color")]

        private Color defaultGizmoColor = new Color(1f, 0f, 0f, 0.5f);

        private void OnDrawGizmosSelected()
        {
            if (enabled == false)
            {
                return;
            }

            if (drawGizmo == false)
            {
                return;
            }

            Handles.color = defaultGizmoColor;

            var center = transform.position;

            var start = Quaternion.Euler(0f, -angle * 0.5f, 0f) * transform.forward;

            if (isWireGizmo == true)
            {
                var end = Quaternion.Euler(0f, angle * 0.5f, 0f) * transform.forward;

                Handles.DrawWireArc(center, Vector3.up, start, angle, radius);

                Handles.DrawLine(center, center + start * radius);

                Handles.DrawLine(center, center + end * radius);
            }

            else
            {
                Handles.DrawSolidArc(center, Vector3.up, start, angle, radius);
            }
        }

        #endif

        private void Start()
        {

        }

        public bool Detect(Transform target)
        {
            if (enabled == false)
            {
                return false;
            }

            Vector3 direction = target.position - transform.position;

            if (direction.magnitude > radius)
            {
                return false;
            }

            float dot = Vector3.Dot(direction.normalized, transform.forward);

            float acos = Mathf.Acos(dot);

            float degree = Mathf.Rad2Deg * acos;

            if (degree > angle * 0.5f)
            {
                return false;
            }

            return true;
        }
    }
}