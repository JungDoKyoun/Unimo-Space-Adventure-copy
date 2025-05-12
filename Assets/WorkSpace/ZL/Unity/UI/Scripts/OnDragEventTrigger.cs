using UnityEngine;

using UnityEngine.EventSystems;

using UnityEngine.Events;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/On Drag Event Trigger")]

    [DisallowMultipleComponent]

    public sealed class OnDragEventTrigger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [Space]

        [SerializeField]

        private RectTransform rect;

        [Space]

        [SerializeField]

        private float dragSensitivity = 0.01f;

        [SerializeField]

        private float maxMagnitude = 10f;

        [Space]

        [SerializeField]
        
        [UsingCustomProperty]

        [ReadOnly(true)]

        private Vector2 startPoint;

        public Vector2 StartPoint
        {
            get => startPoint;
        }

        [SerializeField]

        [UsingCustomProperty]

        [ReadOnly(true)]

        private Vector2 endPoint;

        public Vector2 EndPoint
        {
            get => endPoint;
        }

        [Space]

        [SerializeField]
        
        [UsingCustomProperty]

        [ReadOnly(true)]

        private Vector2 dragDirection;

        public Vector2 DragDirection
        {
            get => dragDirection;
        }

        [Space]

        [SerializeField]

        private UnityEvent<Vector2> onDragEvent;

        public UnityEvent<Vector2> OnDragEvent
        {
            get => onDragEvent;
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

        [Alias("Color")]

        private Color gizmoColor = new Color(0f, 1f, 0f, 0.5f);

        private void OnDrawGizmos()
        {
            if (drawGizmo == false)
            {
                return;
            }

            Gizmos.color = gizmoColor;

            Gizmos.DrawLine(startPoint, endPoint);
        }

#endif

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.TryGetLocalPoint(rect, out startPoint) == false)
            {
                return;
            }

            OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            dragDirection = Vector2.zero;

            onDragEvent.Invoke(dragDirection);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.TryGetLocalPoint(rect, out endPoint) == false)
            {
                return;
            }

            dragDirection = (endPoint - startPoint) * dragSensitivity;

            dragDirection = Vector2.ClampMagnitude(dragDirection, maxMagnitude);

            onDragEvent.Invoke(dragDirection);
        }
    }
}