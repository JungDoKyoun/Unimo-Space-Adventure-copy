using UnityEngine;

using ZL.Unity.Collections;

namespace ZL.Unity.Audio
{
    [AddComponentMenu("ZL/Audio/SFX Player")]

    [DisallowMultipleComponent]

    [RequireComponent(typeof(AudioSource))]

    public sealed class SFXPlayer : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [ReadOnly(true)]

        private AudioSource audioSource;

        [Space]

        [SerializeField]

        private SerializableDictionary<string, AudioClip> audioClipDictionary;

        public void Play(string audioClipName)
        {
            audioSource.clip = audioClipDictionary[audioClipName];

            audioSource.Play();
        }

        public void PlayOneShot(string audioClipName)
        {
            audioSource.PlayOneShot(audioClipDictionary[audioClipName]);
        }
    }
}