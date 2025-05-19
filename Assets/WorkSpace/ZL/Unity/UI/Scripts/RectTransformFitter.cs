using UnityEngine;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Rect Transform Fitter")]

    public sealed class RectTransformFitter : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        private RectTransform rectTransform = null;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [Button(nameof(FitParentSizeMin))]

        [Button(nameof(FitParentSizeMax))]

        [ReadOnlyWhenPlayMode]

        private RectTransform fitTarget = null;

        public void FitParentSizeMin()
        {
            rectTransform.FitSizeMin(fitTarget.sizeDelta);
        }

        public void FitParentSizeMax()
        {
            rectTransform.FitSizeMax(fitTarget.sizeDelta);
        }
    }
}