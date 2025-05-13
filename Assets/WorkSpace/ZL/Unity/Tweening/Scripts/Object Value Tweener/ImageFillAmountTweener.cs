using DG.Tweening.Plugins.Options;

using UnityEngine;

using UnityEngine.UI;

namespace ZL.Unity.Tweening
{
    [AddComponentMenu("ZL/Tweening/Image Fill Amount Tweener")]

    [RequireComponent(typeof(Image))]

    public sealed class ImageFillAmountTweener : ObjectValueTweener<FloatTweener, float, float, FloatOptions>
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [ReadOnly(true)]

        private Image image;

        public override float Value
        {
            get => image.fillAmount;

            set => image.fillAmount = value;
        }
    }
}