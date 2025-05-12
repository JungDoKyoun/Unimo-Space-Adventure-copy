using DG.Tweening.Plugins.Options;

using UnityEngine;

namespace ZL.Unity.Tweening
{
    [AddComponentMenu("ZL/Tweening/Light Intensity Tweener")]

    [RequireComponent(typeof(Light))]

    public sealed class LightIntensityTweener : ObjectValueTweener<FloatTweener, float, float, FloatOptions>
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [ReadOnly(true)]

        #pragma warning disable CS0108

        private Light light;

        #pragma warning restore CS0108

        public override float Value
        {
            get => light.intensity;
            
            set => light.intensity = value;
        }
    }
}