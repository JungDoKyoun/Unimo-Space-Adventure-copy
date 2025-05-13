using DG.Tweening.Core;

using DG.Tweening.Plugins.Options;

using UnityEngine;

namespace ZL.Unity.Tweening
{
    [AddComponentMenu("ZL/Tweening/Material Alpha Tweener")]

    public sealed class MaterialAlphaTweener : ObjectValueTweener<FloatTweener, float, float, FloatOptions>
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [ReadOnly(true)]

        private MaterialController materialController = null;

        [SerializeField]

        [UsingCustomProperty]

        [ToggleIf(nameof(materialController), null, false)]

        [Margin]

        private Material material = null;

        public Material Material
        {
            get => material;

            set => material = value;
        }

        private Color color;

        public override float Value
        {
            get => color.a;

            set
            {
                color.a = value;

                material.color = color;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            if (materialController.Material != null)
            {
                material = materialController.Material;
            }

            color = material.color;
        }

        public override TweenerCore<float, float, FloatOptions> Tween(float endValue, float duration = -1f)
        {
            color = material.color;

            return valueTweener.Tween(endValue, duration);
        }
    }
}