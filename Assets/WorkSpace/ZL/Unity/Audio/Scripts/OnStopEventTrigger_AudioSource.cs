using UnityEngine;

namespace ZL.Unity.Audio
{
    [AddComponentMenu("ZL/Audio/On Stop Event Trigger (Audio Source)")]

    public sealed class OnStopEventTrigger_AudioSource : OnStopEventTrigger
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        private AudioSource audioSource = null;

        protected override bool IsStoped
        {
            get => !audioSource.isPlaying;
        }
    }
}