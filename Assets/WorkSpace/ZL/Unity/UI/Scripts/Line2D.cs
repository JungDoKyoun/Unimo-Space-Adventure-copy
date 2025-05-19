using UnityEngine;

using UnityEngine.UI;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Line 2D")]

    [ExecuteInEditMode]

    public sealed class Line2D : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        private RectTransform rectTransform = null;

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        private Image image = null;

        [Space]

        [SerializeField]

        private Vector2 startPoint = Vector2.zero;

        public Vector2 StartPoint
        {
            get => startPoint;

            set
            {
                startPoint = value;

                rectTransform.localPosition = startPoint;
            }
        }

        [SerializeField]

        private Vector2 endPoint = Vector2.zero;

        public Vector2 EndPoint
        {
            get => endPoint;

            set
            {
                endPoint = value;

                transform.localRotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(endPoint.y - transform.localPosition.y, endPoint.x - transform.localPosition.x) * Mathf.Rad2Deg);

                sizeDelta.x = Vector2.Distance(transform.localPosition, endPoint);

                rectTransform.sizeDelta = sizeDelta;
            }
        }

        [Space]

        [SerializeField]

        private float thickness = 0f;

        public float Thickness
        {
            get => thickness;

            set
            {
                thickness = value;

                sizeDelta.y = thickness;

                rectTransform.sizeDelta = sizeDelta;
            }
        }

        private Vector2 sizeDelta = Vector2.zero;

        public Color Color
        {
            get => image.color;

            set => image.color = value;
        }

        #if UNITY_EDITOR

        private static readonly DrivenTransformProperties drivenProperties = DrivenTransformProperties.AnchoredPosition3D | DrivenTransformProperties.Anchors | DrivenTransformProperties.Rotation | DrivenTransformProperties.SizeDelta | DrivenTransformProperties.Pivot;

        private DrivenRectTransformTracker drivenTracker = new DrivenRectTransformTracker();

        private void Awake()
        {
            if (rectTransform == null)
            {
                rectTransform = GetComponent<RectTransform>();
            }
        }

        private void OnEnable()
        {
            drivenTracker.Add(this, rectTransform, drivenProperties);
        }

        private void OnDisable()
        {
            drivenTracker.Clear();
        }

        private void Reset()
        {
            if (rectTransform == null)
            {
                rectTransform = GetComponent<RectTransform>();
            }
        }

        private void OnValidate()
        {
            if (rectTransform == null)
            {
                rectTransform = GetComponent<RectTransform>();
            }

            rectTransform.hasChanged = true;
        }

        private void LateUpdate()
        {
            if (rectTransform.hasChanged == true)
            {
                rectTransform.hasChanged = false;

                Refresh();
            }
        }

        #else

        private void Awake()
        {
            Refresh();
        }

        #endif

        private void Refresh()
        {
            rectTransform.hasChanged = false;

            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);

            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);

            rectTransform.pivot = new Vector2(0f, 0.5f);

            StartPoint = startPoint;

            EndPoint = endPoint;

            Thickness = thickness;
        }
    }
}