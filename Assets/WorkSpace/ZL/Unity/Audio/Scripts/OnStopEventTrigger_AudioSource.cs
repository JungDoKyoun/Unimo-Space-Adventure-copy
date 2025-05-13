using UnityEngine;

namespace ZL.Unity.Audio
{
    [AddComponentMenu("ZL/Audio/On Stop Event Trigger (Audio Source)")]

    [RequireComponent(typeof(AudioSource))]

    public sealed class OnStopEventTrigger_AudioSource : OnStopEventTrigger<AudioSource>
    {
        public override bool IsStoped
        {
            get => !target.isPlaying;
        }
    }
}