#if UNITY_EDITOR

using UnityEditor;

#endif

using UnityEngine;

using UnityEngine.Audio;

using ZL.Unity.Collections;

using ZL.Unity.IO;

using ZL.Unity.Singleton;

namespace ZL.Unity.Audio
{
    [AddComponentMenu("ZL/Audio/Audio Mixer Manager (Singleton)")]

    [DefaultExecutionOrder(-1)]

    public sealed class AudioMixerManager : MonoSingleton<AudioMixerManager>
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [PropertyField]

        [Essential]

        [Button("LoadAudioMixerParameters", "Load Parameters")]

        private AudioMixer audioMixer = null;

        [SerializeField]

        [UsingCustomProperty]

        [PropertyField]

        [Button(nameof(LoadVolumes))]

        [Button(nameof(SaveVolumes))]

        private SerializableDictionary<string, float, FloatPref> parameterPrefs;

        #if UNITY_EDITOR

        private void OnValidate()
        {
            foreach (var parameterPref in parameterPrefs)
            {
                parameterPref.Value = Mathf.Clamp01(parameterPref.Value);

                if (Application.isPlaying == true)
                {
                    SetVolume(parameterPref.Key, parameterPref.Value);
                }
            }
        }

        private void LoadAudioMixerParameters()
        {
            parameterPrefs.Clear();

            if (audioMixer != null)
            {
                foreach (var audioMixerGroup in audioMixer.FindMatchingGroups(string.Empty))
                {
                    var key = audioMixerGroup.name;

                    audioMixer.GetFloat(key, out float value);

                    var volumePref = new FloatPref(key, value);

                    volumePref.TryLoadValue();

                    parameterPrefs.Add(volumePref);
                }
            }

            EditorUtility.SetDirty(this);
        }

        #endif

        private void Start()
        {
            foreach (var parameterPref in parameterPrefs)
            {
                parameterPref.OnValueChangedAction += (value) =>
                {
                    audioMixer.SetVolume(parameterPref.Key, value);
                };
            }

            LoadVolumes();
        }

        public void LoadVolumes()
        {
            foreach (var parameterPref in parameterPrefs)
            {
                parameterPref.TryLoadValue();
            }
        }

        public void SaveVolumes()
        {
            foreach (var parameterPref in parameterPrefs)
            {
                parameterPref.SaveValue();
            }
        }

        public float GetVolume(string key)
        {
            return parameterPrefs[key];
        }

        public void SetVolume(string key, float value)
        {
            parameterPrefs[key] = value;
        }
    }
}