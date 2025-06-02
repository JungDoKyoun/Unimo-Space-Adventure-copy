using UnityEngine;

using UnityEngine.EventSystems;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Draggable Rect")]

    public sealed class DraggableRect : MonoBehaviour, IDragHandler
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        private RectTransform rectTransform;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponentInParent]

        [Essential]

        [ReadOnly(true)]

        private Canvas canvas;

        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }
}