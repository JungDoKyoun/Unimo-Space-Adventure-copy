using UnityEngine;

using UnityEngine.EventSystems;

namespace ZL.Unity.UI
{
    public static class PointerEventDataExtensions
    {
        public static bool TryGetLocalPoint(this PointerEventData instance, RectTransform rect, out Vector2 localPoint)
        {
            return RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, instance.position, instance.pressEventCamera, out localPoint);
        }
    }
}