using UnityEngine;

using UnityEngine.UI;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Image Controller")]

    [DisallowMultipleComponent]

    [RequireComponent(typeof(Image))]

    public sealed class ImageController : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [ReadOnly(true)]

        private Image image;

        [Space]

        [SerializeField]
        
        [Range(0f, 1f)]

        private float alphaHitTestMinimumThreshold = 0f;

        private void Awake()
        {
            image.alphaHitTestMinimumThreshold = alphaHitTestMinimumThreshold;
        }

        private void OnValidate()
        {
            if (Application.isPlaying == true)
            {
                image.alphaHitTestMinimumThreshold = alphaHitTestMinimumThreshold;
            }
        }

        #if UNITY_EDITOR

        private void Update()
        {
            alphaHitTestMinimumThreshold = image.alphaHitTestMinimumThreshold;
        }

        #endif
    }
}