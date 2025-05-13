using UnityEngine;

namespace ZL.Unity.Tweening
{
    [AddComponentMenu("ZL/Tweening/Canvas Group Fader Group")]

    [DisallowMultipleComponent]

    public sealed class CanvasGroupFaderGroup : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private CanvasGroupFader main;

        [Space]

        [SerializeField]

        private bool fadeOutCurrent = true;

        [SerializeField]

        private bool sortSibling = true;

        private CanvasGroupFader current = null;

        public void FadeInMain()
        {
            main?.FadeIn();
        }

        public void SwapCurrent(CanvasGroupFader newCurrent)
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