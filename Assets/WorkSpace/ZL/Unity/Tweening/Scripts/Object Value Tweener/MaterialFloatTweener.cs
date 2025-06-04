using DG.Tweening.Plugins.Options;

using UnityEngine;

using ZL.Unity.GFX;

namespace ZL.Unity.Tweening
{
    [AddComponentMenu("ZL/Tweening/Material Float Tweener")]

    public sealed class MaterialFloatTweener : ObjectValueTweener<FloatTweener, float, float, FloatOptions>
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

        [SerializeField]

        private string propertyName = "";

        public override float Value
        {
            get => material.GetFloat(propertyName);

            set => material.SetFloat(propertyName, value);
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