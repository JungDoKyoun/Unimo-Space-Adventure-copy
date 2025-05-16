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

        [ReadOnlyWhenPlayMode]

        private ObjectValueTweener<FloatTweener, float, float, FloatOptions> tweener = null;

        [SerializeField]

        [UsingCustomProperty]

        [GetComponentInParentOnly]

        [EmptyField]

        private FaderGroup faderGroup = null;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [PropertyField]

        [ReadOnlyWhenEditMode]

        [Button(nameof(ToggleFaded))]

        private bool isFadedIn = false;

        [Space]

        [SerializeField]

        private UnityEvent onFadeInEvent = null;

        public UnityEvent OnFadeInEvent => onFadeInEvent;

        [Space]

        [SerializeField]

        private UnityEvent onFadedInEvent = null;

        public UnityEvent OnFadedInEvent => onFadedInEvent;

        [Space]

        [SerializeField]

        private UnityEvent onFadeOutEvent = null;

        public UnityEvent OnFadeOutEvent => onFadeOutEvent;

        [Space]

        [SerializeField]

        private UnityEvent onFadedOutEvent = null;

        public UnityEvent OnFadedOutEvent => onFadedOutEvent;

        private void Start()
        {
            if (isFadedIn == true)
            {
                tweener.Value = 1f;

                gameObject.SetActive(true);
            }

            else
            {
                tweener.Value = 0f;

                gameObject.SetActive(false);
            }
        }

        public void ToggleFaded()
        {
            if (isFadedIn == true)
            {
                FadeOut();
            }

            else
            {
                FadeIn();
            }
        }

        /// <summary>
        /// Fade alpha from 0 to 1.<br/>
        /// 알파를 0에서 1으로 페이드합니다.<br/>
        /// </summary>
        /// <param name="duration">
        /// Fade time;<br/>
        /// -1 = Use tweener defaults<br/>
        /// 페이드 시간;<br/>
        /// -1 = Tweener 기본값 사용<br/>
        /// </param>
        public void FadeIn(float duration = -1f)
        {
            faderGroup?.SwapCurrent(this);

            gameObject.SetActive(true);

            isFadedIn = true;

            onFadeInEvent.Invoke();

            tweener.Tween(1f, duration).OnComplete(OnFadedIn);
        }

        private void OnFadedIn()
        {
            OnFadedInEvent.Invoke();
        }

        /// <summary>
        /// Fade alpha from 1 to 0.<br/>
        /// 알파를 1에서 0으로 페이드합니다.<br/>
        /// </summary>
        /// <param name="duration">
        /// Fade time;<br/>
        /// -1 = Use tweener defaults<br/>
        /// 페이드 시간;<br/>
        /// -1 = Tweener 기본값 사용<br/>
        /// </param>
        public void FadeOut(float duration = -1f)
        {
            isFadedIn = false;

            onFadeOutEvent.Invoke();

            tweener.Tween(0f, duration).OnComplete(OnFadedOut);
        }

        private void OnFadedOut()
        {
            OnFadedOutEvent.Invoke();

            gameObject.SetActive(false);
        }
    }
}