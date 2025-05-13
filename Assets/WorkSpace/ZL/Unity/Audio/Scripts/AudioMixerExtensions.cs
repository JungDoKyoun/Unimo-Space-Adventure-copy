using UnityEngine.Audio;

using ZL.CS;

namespace ZL.Unity.Audio
{
    public static partial class AudioMixerExtensions
    {
        public static void SetVolume(this AudioMixer instance, string key, float value)
        {
            instance.SetFloat(key, MathFEx.LinearToDecibel(value));
        }

        public static bool TryGetVolume(this AudioMixer instance, string key, out float volume)
        {
            if (instance.GetFloat(key, out volume) == false)
            {
                volume = 0f;

                return false;
            }

            volume = MathFEx.DecibelToLinear(volume);

            return true;
        }
    }
}