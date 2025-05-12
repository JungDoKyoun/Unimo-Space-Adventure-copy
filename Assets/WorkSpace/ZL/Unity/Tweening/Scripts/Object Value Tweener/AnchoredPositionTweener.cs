using DG.Tweening.Plugins.Options;

using UnityEngine;

namespace ZL.Unity.Tweening
{
    [AddComponentMenu("ZL/Tweening/Anchored Position Tweener")]

    [RequireComponent(typeof(RectTransform))]

    public sealed class AnchoredPositionTweener : ObjectValueTweener<Vector2Tweener, Vector2, Vector2, VectorOptions>
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [ReadOnly(true)]

        private RectTransform rectTransform;

        public override Vector2 Value
        {
            get => rectTransform.anchoredPosition;

            set => rectTransform.anchoredPosition = value;
        }
    }
}