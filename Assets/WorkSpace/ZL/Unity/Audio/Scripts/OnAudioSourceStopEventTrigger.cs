using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity.Audio
{
    [AddComponentMenu("ZL/Audio/On Audio Source Stop Event Trigger")]

    public sealed class OnAudioSourceStopEventTrigger : OnStopEventTrigger
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        private AudioSource audioSource = null;

        [Space]

        [SerializeField]

        private UnityEvent onStopEvent = null;

        public override UnityEvent OnStopEvent
        {
            get => onStopEvent;
        }

        protected override bool IsStoped
        {
            get => !audioSource.isPlaying;
        }
    }
}