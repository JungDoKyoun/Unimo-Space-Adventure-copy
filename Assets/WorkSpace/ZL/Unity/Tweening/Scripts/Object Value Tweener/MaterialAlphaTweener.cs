using DG.Tweening.Plugins.Options;

using UnityEngine;

using ZL.Unity.GFX;

namespace ZL.Unity.Tweening
{
    [AddComponentMenu("ZL/Tweening/Material Alpha Tweener")]

    public sealed class MaterialAlphaTweener : ObjectValueTweener<FloatTweener, float, float, FloatOptions>
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [Essential]

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

        private Color color = Color.white;

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

        public override void Play()
        {
            color = material.color;

            base.Play();
        }
    }
}