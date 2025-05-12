using UnityEngine;

using UnityEngine.EventSystems;

using UnityEngine.Events;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Joystick")]

    [DisallowMultipleComponent]

    public sealed class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [SerializeField]

        private RectTransform container = null;

        [SerializeField]

        private RectTransform handle = null;

        [SerializeField]

        [UsingCustomProperty]

        [ReadOnly(true)]

        private Vector2 dragDirection = Vector2.zero;

        [SerializeField]

        private float dragRange = 75f;

        [SerializeField]

        private UnityEvent<Vector2> onDragEvent = null;

        public Vector2 DragDirection
        {
            get => dragDirection;
        }

        private void Start()
        {
            if (handle != null)
            {
                handle.anchoredPosition = Vector2.zero;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            dragDirection = Vector2.zero;

            onDragEvent.Invoke(Vector2.zero);

            if (handle != null)
            {
                handle.anchoredPosition = Vector2.zero;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.TryGetLocalPoint(container, out var pointerPosition) == false)
            {
                return;
            }

            if (handle != null)
            {
                if (pointerPosition.magnitude < dragRange)
                {
                    handle.anchoredPosition = pointerPosition;
                }

                else
                {
                    handle.anchoredPosition = pointerPosition.normalized * dragRange;
                }
            }

            dragDirection = pointerPosition / container.sizeDelta;

            onDragEvent.Invoke(dragDirection);
        }
    }
}