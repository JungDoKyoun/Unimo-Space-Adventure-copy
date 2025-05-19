// Working

using UnityEngine;

namespace ZL.Unity.Tweening
{
    [AddComponentMenu("ZL/Tweening/Fader Group")]

    public sealed class FaderGroup : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private Fader main = null;

        [Space]

        [SerializeField]

        private bool fadeOutCurrent = true;

        [SerializeField]

        private bool sortSibling = true;

        private Fader current = null;

        public void FadeInMain()
        {
            main?.FadeIn();
        }

        public void SwapCurrent(Fader newCurrent)
        {
            if (fadeOutCurrent == true)
            {
                FadeOutCurrent();
            }

            current = newCurrent;

            if (sortSibling == true)
            {
                current.transform.SetAsLastSibling();
            }
        }

        public void FadeOutCurrent()
        {
            current?.FadeOut();
        }
    }
}