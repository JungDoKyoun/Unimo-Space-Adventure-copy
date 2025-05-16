using DG.Tweening.Plugins.Options;

using UnityEngine;

using UnityEngine.UI;

namespace ZL.Unity.Tweening
{
    [AddComponentMenu("ZL/Tweening/Image Fill Amount Tweener")]

    public sealed class ImageFillAmountTweener : ObjectValueTweener<FloatTweener, float, float, FloatOptions>
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        private Image image = null;

        public override float Value
        {
            get => image.fillAmount;

            set => image.fillAmount = value;
        }
    }
}