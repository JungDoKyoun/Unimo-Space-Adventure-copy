using DG.Tweening;

using DG.Tweening.Core;

using DG.Tweening.Plugins.Options;

using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity.Tweening
{
    [DefaultExecutionOrder((int)ScriptExecutionOrder.Tweener)]

    public abstract class ObjectValueTweener<TValueTweener, T1, T2, TPlugOptions> : MonoBehaviour

        where TValueTweener : ValueTweener<T1, T2, TPlugOptions>

        where TPlugOptions : struct, IPlugOptions
    {
        [Space]

        [SerializeField]

        private TValueTweener valueTweener = null;

        public TValueTweener ValueTweener
        {
            get => valueTweener;
        }

        public UnityEvent OnStartEvent
        {
            get => ValueTweener.OnStartEvent;
        }

        public UnityEvent OnCompleteEvent
        {
            get => ValueTweener.OnCompleteEvent;
        }

        public TweenerCore<T1, T2, TPlugOptions> Current
        {
            get => ValueTweener.Current;
        }

        public abstract T1 Value { get; set; }

        protected virtual void Awake()
        {
            FixedDebug.Log($"(Tweening) {name} Awaken");

            ValueTweener.Getter = () => Value;

            ValueTweener.Setter = (value) => Value = value;
        }

        public void SetEndValue(T2 endValue)
        {
            ValueTweener.SetEndValue(endValue);
        }

        public void SetDuration(float duration)
        {
            ValueTweener.SetDuration(duration);
        }

        public void SetDelay(float delay)
        {
            ValueTweener.SetDelay(delay);
        }

        public void SetEase(int ease)
        {
            ValueTweener.SetEase((Ease)ease);
        }

        public void SetEase(Ease ease)
        {
            ValueTweener.SetEase(ease);
        }

        public void SetLoops(int loops)
        {
            ValueTweener.SetLoops(loops);
        }

        public void SetLoopType(int loopType)
        {
            ValueTweener.SetLoopType((LoopType)loopType);
        }

        public void SetLoopType(LoopType loopType)
        {
            ValueTweener.SetLoopType(loopType);
        }

        public virtual void Play()
        {
            ValueTweener.Play();
        }
    }
}