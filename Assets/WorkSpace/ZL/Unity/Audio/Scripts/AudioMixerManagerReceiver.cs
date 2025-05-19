using UnityEngine;

namespace ZL.Unity.Audio
{
    [AddComponentMenu("ZL/Audio/Audio Mixer Manager Receiver")]

    public sealed class AudioMixerManagerReceiver : MonoBehaviour
    {
        public void LoadVolumes()
        {
            AudioMixerManager.Instance.LoadVolumes();
        }

        public void SaveVolumes()
        {
            AudioMixerManager.Instance.SaveVolumes();
        }

        public float GetVolume(string key)
        {
            return AudioMixerManager.Instance.GetVolume(key);
        }

        public void SetVolume(string key, float value)
        {
            AudioMixerManager.Instance.SetVolume(key, value);
        }
    }
}