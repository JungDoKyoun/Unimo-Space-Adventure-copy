using UnityEngine;

using UnityEngine.UI;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Image Repeater")]

    [DisallowMultipleComponent]
    
    [RequireComponent(typeof(Image))]

    public sealed class ImageRepeater : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [ReadOnly(true)]

        private RectTransform rectTransform;

        [Space]

        [SerializeField]

        private float pixelPerSecond = 0f;

        private Vector3 originalPosition;

        private float value = 0f;

        private void Update()
        {
            transform.localPosition = originalPosition + Vector3.right * Mathf.Repeat(value, rectTransform.rect.width);

            value += pixelPerSecond * Time.deltaTime;
        }
    }
}