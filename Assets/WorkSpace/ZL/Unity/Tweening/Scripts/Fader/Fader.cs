using DG.Tweening;

using DG.Tweening.Plugins.Options;

using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity.Tweening
{
    [AddComponentMenu("ZL/Tweening/Fader")]

    public sealed class Fader : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [Essential]

        [PropertyField]

        [Margin]

        [ReadOnlyWhenEditMode]

        [Button(nameof(FadeIn))]

        [Button(nameof(FadeOut))]

        private ObjectValueTweener<FloatTweener, float, float, FloatOptions> tweener = null;

        [SerializeField]

        [UsingCustomProperty]

        [GetComponentInParentOnly]

        [EmptyField]

        private FaderGroup faderGroup = null;

        [Space]

        [SerializeField]

        private UnityEvent onFadeInEvent = null;

        public UnityEvent OnFadeInEvent
        {
            get => onFadeInEvent;
        }

        [Space]

        [SerializeField]

        private UnityEvent onFadedInEvent = null;

        public UnityEvent OnFadedInEvent
        {
            get => onFadedInEvent;
        }

        [Space]

        [SerializeField]

        private UnityEvent onFadeOutEvent = null;

        public UnityEvent OnFadeOutEvent
        {
            get => onFadeOutEvent;
        }

        [Space]

        [SerializeField]

        private UnityEvent onFadedOutEvent = null;

        public UnityEvent OnFadedOutEvent
        {
            get => onFadedOutEvent;
        }

        /// <summary>
        /// Fade alpha from 0 to 1.<br/>
        /// 알파를 0에서 1으로 페이드합니다.<br/>
        /// </summary>
        /// <param name = "duration">
        /// Fade time;<br/>
        /// -1 = Use tweener defaults<br/>
        /// 페이드 시간;<br/>
        /// -1 = Tweener 기본값 사용<br/>
        /// </param>
        public void FadeIn()
        {
            faderGroup?.SwapCurrent(this);

            gameObject.SetActive(true);

            onFadeInEvent.Invoke();

            tweener.SetEndValue(1f);

            tweener.Play();

            tweener.Current.OnComplete(OnFadedIn);
        }

        private void OnFadedIn()
        {
            OnFadedInEvent.Invoke();
        }

        /// <summary>
        /// Fade alpha from 1 to 0.<br/>
        /// 알파를 1에서 0으로 페이드합니다.<br/>
        /// </summary>
        /// <param name = "duration">
        /// Fade time;<br/>
        /// -1 = Use tweener defaults<br/>
        /// 페이드 시간;<br/>
        /// -1 = Tweener 기본값 사용<br/>
        /// </param>
        public void FadeOut()
        {
            onFadeOutEvent.Invoke();

            tweener.SetEndValue(0f);

            tweener.Play();

            tweener.Current.OnComplete(OnFadedOut);
        }

        private void OnFadedOut()
        {
            OnFadedOutEvent.Invoke();

            gameObject.SetActive(false);
        }
    }
}