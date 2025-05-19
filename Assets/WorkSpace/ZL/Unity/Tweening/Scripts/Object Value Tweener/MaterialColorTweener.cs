using DG.Tweening.Plugins.Options;

using UnityEngine;

using ZL.Unity.GFX;

namespace ZL.Unity.Tweening
{
    [AddComponentMenu("ZL/Tweening/Material Color Tweener")]

    public sealed class MaterialColorTweener : ObjectValueTweener<ColorTweener, Color, Color, ColorOptions>
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [ReadOnly(true)]

        private MaterialController materialController = null;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [ToggleIf(nameof(materialController), null, false)]

        private Material material = null;

        public Material Material
        {
            get => material;

            set => material = value;
        }

        public override Color Value
        {
            get => material.color;

            set => material.color = value;
        }

        protected override void Awake()
        {
            base.Awake();

            if (materialController.Material != null)
            {
                material = materialController.Material;
            }
        }
    }
}