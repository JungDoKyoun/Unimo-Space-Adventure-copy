using UnityEngine;

namespace ZL.Unity.UI
{
    public static partial class RectTransformExtensions
    {
        public static void LookAt2D(this Transform instance, in Vector2 worldUp)
        {
            instance.localRotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(worldUp.y - instance.localPosition.y, worldUp.x - instance.localPosition.x) * Mathf.Rad2Deg);
        }

        public static void SetAnchorStrech(this RectTransform instance)
        {
            instance.anchorMin = Vector2.zero;

            instance.anchorMax = Vector2.one;

            instance.offsetMin = Vector2.zero;

            instance.offsetMax = Vector2.zero;
        }

        public static void FitSizeMin(this RectTransform instance, Vector2 sizeDelta)
        {
            if (instance.sizeDelta.x / instance.sizeDelta.y > sizeDelta.x / sizeDelta.y)
            {
                sizeDelta.y = sizeDelta.x * instance.sizeDelta.y / instance.sizeDelta.x;
            }

            else
            {
                sizeDelta.x = sizeDelta.y * instance.sizeDelta.x / instance.sizeDelta.y;
            }

            instance.anchoredPosition = Vector2.zero;

            instance.sizeDelta = sizeDelta;
        }

        public static void FitSizeMax(this RectTransform instance, Vector2 sizeDelta)
        {
            if (instance.sizeDelta.x / instance.sizeDelta.y > sizeDelta.x / sizeDelta.y)
            {
                sizeDelta.x = sizeDelta.y * instance.sizeDelta.x / instance.sizeDelta.y;
            }

            else
            {
                sizeDelta.y = sizeDelta.x * instance.sizeDelta.y / instance.sizeDelta.x;
            }

            instance.anchoredPosition = Vector2.zero;

            instance.sizeDelta = sizeDelta;
        }

        public static void Line(this RectTransform instance, in Vector2 endPosition, float thickness)
        {
            Vector2 sizeDelta = Vector3.zero;

            Vector3 localRotation = Vector3.zero;

            sizeDelta.x = Vector2.Distance(instance.localPosition, endPosition);

            sizeDelta.y = thickness;

            instance.sizeDelta = sizeDelta;

            localRotation.z = Mathf.Atan2(endPosition.y - instance.localPosition.y, endPosition.x - instance.localPosition.x) * Mathf.Rad2Deg;

            instance.localRotation = Quaternion.Euler(localRotation);
        }
    }
}