using DG.Tweening.Plugins.Options;

using UnityEngine;

namespace ZL.Unity.Tweening
{
    [AddComponentMenu("ZL/Tweening/Light Intensity Tweener")]

    public sealed class LightIntensityTweener : ObjectValueTweener<FloatTweener, float, float, FloatOptions>
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        #pragma warning disable CS0108

        private Light light = null;

        #pragma warning restore CS0108

        public override float Value
        {
            get => light.intensity;
            
            set => light.intensity = value;
        }
    }
}