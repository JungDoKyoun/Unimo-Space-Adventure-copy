using DG.Tweening.Plugins.Options;

using UnityEngine;

namespace ZL.Unity.Tweening
{
    [AddComponentMenu("ZL/Tweening/Audio Source Tweener")]

    [RequireComponent(typeof(AudioSource))]

    public sealed class AudioSourceVolumeTweener : ObjectValueTweener<FloatTweener, float, float, FloatOptions>
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [ReadOnly(true)]

        private AudioSource audioSource;

        public override float Value
        {
            get => audioSource.volume;
            
            set => audioSource.volume = value;
        }
    }
}