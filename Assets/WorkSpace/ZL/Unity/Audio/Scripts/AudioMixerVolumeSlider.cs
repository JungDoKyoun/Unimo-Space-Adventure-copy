using UnityEngine;

using UnityEngine.UI;

namespace ZL.Unity.Audio
{
    [AddComponentMenu("ZL/UI/Audio Mixer Volume Slider")]

    public sealed class AudioMixerVolumeSlider : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [ReadOnlyWhenPlayMode]

        [Essential]

        private Slider slider = null;

        [SerializeField]
        
        [UsingCustomProperty]
        
        [ReadOnlyWhenPlayMode]

        private string key = "";

        public void Refresh()
        {
            slider.value = AudioMixerManager.Instance.GetVolume(key) * 100f;
        }

        public void SetVolume(float value)
        {
            AudioMixerManager.Instance.SetVolume(key, value * 0.01f);
        }
    }
}