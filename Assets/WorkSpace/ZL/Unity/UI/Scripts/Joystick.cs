using UnityEngine;

using UnityEngine.EventSystems;

using UnityEngine.Events;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Joystick")]

    public sealed class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private RectTransform container = null;

        [SerializeField]

        [UsingCustomProperty]

        [ReadOnlyWhenPlayMode]

        private RectTransform handle = null;

        [Space]

        [SerializeField]

        private float dragRange = 75f;

        [Space]

        [SerializeField]

        private UnityEvent<Vector2> onDragEvent = null;

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
            onDragEvent.Invoke(Vector2.zero);

            if (handle != null)
            {
                handle.anchoredPosition = Vector2.zero;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.TryGetLocalPoint(container, out var localPoint) == false)
            {
                return;
            }

            if (handle != null)
            {
                if (localPoint.magnitude < dragRange)
                {
                    handle.anchoredPosition = localPoint;
                }

                else
                {
                    handle.anchoredPosition = localPoint.normalized * dragRange;
                }
            }

            var dragDirection = localPoint / container.sizeDelta;

            onDragEvent.Invoke(dragDirection);
        }
    }
}