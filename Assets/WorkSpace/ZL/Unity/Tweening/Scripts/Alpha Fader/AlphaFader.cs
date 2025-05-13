using DG.Tweening;

using DG.Tweening.Plugins.Options;

using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity.Tweening
{
    [DisallowMultipleComponent]

    public abstract class AlphaFader<TTweener> : AlphaFader

        where TTweener : ObjectValueTweener<FloatTweener, float, float, FloatOptions>
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [ReadOnly(true)]

        protected TTweener tweener;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [PropertyField]

        [ReadOnlyWhenEditMode]

        [Button("ToggleFaded")]

        protected bool isFadedIn = false;

        public override bool IsFadedIn
        {
            get => isFadedIn;
        }

        [Space]

        [SerializeField]

        protected UnityEvent onFadeInEvent;

        public override UnityEvent OnFadeInEvent => onFadeInEvent;

        [Space]

        [SerializeField]

        protected UnityEvent onFadedInEvent;

        public override UnityEvent OnFadedInEvent => onFadedInEvent;

        [Space]

        [SerializeField]

        protected UnityEvent onFadeOutEvent;

        public override UnityEvent OnFadeOutEvent => onFadeOutEvent;

        [Space]

        [SerializeField]

        protected UnityEvent onFadedOutEvent;

        public override UnityEvent OnFadedOutEvent => onFadedOutEvent;

        private void Awake()
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

        public override void FadeIn(float duration = -1f)
        {
            gameObject.SetActive(true);

            isFadedIn = true;

            onFadeInEvent.Invoke();

            tweener.Tween(1f, duration).OnComplete(OnFadedIn);
        }

        public override void FadeOut(float duration = -1f)
        {
            isFadedIn = false;

            onFadeOutEvent.Invoke();

            tweener.Tween(0f, duration).OnComplete(OnFadedOut);
        }
    }

    public abstract class AlphaFader : MonoBehaviour
    {
        public abstract bool IsFadedIn { get; }

        public abstract UnityEvent OnFadeInEvent { get; }

        public abstract UnityEvent OnFadedInEvent { get; }

        public abstract UnityEvent OnFadeOutEvent { get; }

        public abstract UnityEvent OnFadedOutEvent { get; }

        #if UNITY_EDITOR

        public void ToggleFaded()
        {
            if (IsFadedIn == true)
            {
                FadeOut();
            }

            else
            {
                FadeIn();
            }
        }

        #endif

        public abstract void FadeIn(float duration = -1f);

        protected void OnFadedIn()
        {
            OnFadedInEvent.Invoke();
        }

        public abstract void FadeOut(float duration = -1f);

        protected void OnFadedOut()
        {
            OnFadedOutEvent.Invoke();

            gameObject.SetActive(false);
        }
    }
}