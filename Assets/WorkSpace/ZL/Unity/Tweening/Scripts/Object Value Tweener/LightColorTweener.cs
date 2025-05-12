using DG.Tweening.Plugins.Options;

using UnityEngine;

namespace ZL.Unity.Tweening
{
    [AddComponentMenu("ZL/Tweening/Light Color Tweener")]

    [RequireComponent(typeof(Light))]

    public sealed class LightColorTweener : ObjectValueTweener<ColorTweener, Color, Color, ColorOptions>
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [ReadOnly(true)]

        #pragma warning disable CS0108

        private Light light;

        #pragma warning restore CS0108

        public override Color Value
        {
            get => light.color;
            
            set => light.color = value;
        }
    }
}