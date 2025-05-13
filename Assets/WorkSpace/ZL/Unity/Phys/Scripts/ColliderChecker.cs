using System.Collections;

using UnityEngine;

using ZL.Unity.Coroutines;

namespace ZL.Unity.Phys
{
    [DisallowMultipleComponent]

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

        private bool startCheckingOnEnable = true;

        #if !UNITY_EDITOR

        public bool IsChecked { get; private set; } = false;

        #elif UNITY_EDITOR

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Line(Margin = 0)]

        [Text("<b>Debugging</b>", FontSize = 16)]

        [Margin]

        [ReadOnly(true)]

        private bool isChecked = false;

        public bool IsChecked
        {
            get => isChecked;

            private set => isChecked = value;
        }

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

            if (IsChecked == true)
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

        private void OnEnable()
        {
            if (startCheckingOnEnable == true)
            {
                StartChecking();
            }
        }

        private void OnDisable()
        {
            IsChecked = false;
        }

        protected abstract void DrawGizmos();

        #endif

        public void StartChecking()
        {
            if (checkingRoutine != null)
            {
                return;
            }

            checkingRoutine = CheckingRoutine();

            StartCoroutine(checkingRoutine);
        }

        public void StopChecking()
        {
            if (checkingRoutine == null)
            {
                return;
            }

            StopCoroutine(checkingRoutine);

            checkingRoutine = null;
        }

        private IEnumerator checkingRoutine = null;

        private IEnumerator CheckingRoutine()
        {
            while (true)
            {
                IsChecked = Check();

                yield return WaitForFixedUpdateCache.Get();
            }
        }

        public abstract bool Check();
    }
}