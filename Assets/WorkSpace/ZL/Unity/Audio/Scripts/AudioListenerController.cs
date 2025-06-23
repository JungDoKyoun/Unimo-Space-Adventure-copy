using System;

using UnityEngine;

namespace ZL.Unity.Audio
{
    [AddComponentMenu("ZL/Audio/Audio Listener Controller")]

    public sealed class AudioListenerController : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [Range(0f, 1f)]

        private float volume = 0f;

        [SerializeField]

        private bool pause = false;

        #if UNITY_EDITOR

        private void OnValidate()
        {
            if (Application.isPlaying == false)
            {
                return;
            }

            AudioListener.volume = volume;

            AudioListener.pause = pause;
        }

        private void Update()
        {
            volume = AudioListener.volume;

            pause = AudioListener.pause;
        }

        #endif

        private void Start()
        {
            AudioListener.volume = volume;

            AudioListener.pause = pause;
        }
    }
}