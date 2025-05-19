using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity.Phys
{
    [ExecuteInEditMode]

    public abstract class ColliderChecker : MonoBehaviour
    {
        [Space]

        [SerializeField]

        protected LayerMask layerMask = ~0;

        public LayerMask LayerMask
        {
            get => layerMask;

            set => layerMask = value;
        }

        [SerializeField]

        protected QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.UseGlobal;

        [Space]

        [SerializeField]

        private UnityEvent onEnterEvent;

        public UnityEvent OnEnterEvent
        {
            get => onEnterEvent;
        }

        [Space]

        [SerializeField]

        private UnityEvent onStayEvent;

        public UnityEvent OnStayEvent
        {
            get => onStayEvent;
        }

        [Space]

        [SerializeField]

        private UnityEvent onExitEvent;

        public UnityEvent OnExitEvent
        {
            get => onExitEvent;
        }

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Line(Margin = 0)]

        [Text("<b>Debugging</b>", FontSize = 16)]

        [Margin]

        [ReadOnly(true)]

        private bool isChecked = false;

        #if UNITY_EDITOR

        [Space]

        [SerializeField]

        protected bool drawGizmo = true;

        [SerializeField]

        [UsingCustomProperty]

        [ToggleIf(nameof(drawGizmo), false)]

        [AddIndent(1)]

        [Alias("Is Wire")]

        protected bool isWireGizmo = false;

        [SerializeField]

        [UsingCustomProperty]

        [ToggleIf(nameof(drawGizmo), false)]

        [AddIndent(1)]

        [Alias("Default Color")]

        private Color defaultGizmoColor = new Color(0f, 1f, 0f, 0.5f);

        [SerializeField]

        [UsingCustomProperty]

        [ToggleIf(nameof(drawGizmo), false)]

        [AddIndent(1)]

        [Alias("Collided Color")]

        private Color collidedGizmoColor = new Color(1f, 0f, 0f, 0.5f);

        private void OnDrawGizmos()
        {
            if (drawGizmo == false)
            {
                return;
            }

            if (isChecked == true)
            {
                Gizmos.color = collidedGizmoColor;
            }

            else
            {
                Gizmos.color = defaultGizmoColor;
            }

            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);

            DrawGizmos();
        }

        private void OnDisable()
        {
            isChecked = false;
        }

        protected abstract void DrawGizmos();

#endif

        private void FixedUpdate()
        {
            if (Check() == true)
            {
                if (isChecked == false)
                {
                    isChecked = true;

                    onEnterEvent.Invoke();
                }

                onStayEvent.Invoke();
            }

            else if (isChecked == true)
            {
                isChecked = false;

                onExitEvent.Invoke();
            }
        }

        protected abstract bool Check();
    }
}