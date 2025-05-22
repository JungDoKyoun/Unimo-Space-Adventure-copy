using DG.Tweening;

using DG.Tweening.Core;

using DG.Tweening.Plugins.Options;

using UnityEngine;

namespace ZL.Unity.Tweening
{
    public abstract class ObjectValueTweener<TValueTweener, T1, T2, TPlugOptions> : MonoBehaviour

        where TValueTweener : ValueTweener<T1, T2, TPlugOptions>

        where TPlugOptions : struct, IPlugOptions
    {
        [Space]

        [SerializeField]

        protected TValueTweener valueTweener = null;

        public TValueTweener ValueTweener
        {
            get => valueTweener;
        }

        public float Duration
        {
            get => ValueTweener.Duration;

            set => ValueTweener.Duration = value;
        }

        public float Delay
        {
            get => ValueTweener.Delay;

            set => ValueTweener.Delay = value;
        }

        public Ease Ease
        {
            get => ValueTweener.Ease;

            set => ValueTweener.Ease = value;
        }

        public bool IsIndependentUpdate
        {
            get => ValueTweener.IsIndependentUpdate;

            set => ValueTweener.IsIndependentUpdate = value;
        }

        public int LoopCount
        {
            get => ValueTweener.LoopCount;

            set => ValueTweener.LoopCount = value;
        }

        public LoopType LoopType
        {
            get => ValueTweener.LoopType;

            set => ValueTweener.LoopType = value;
        }

        public TweenerCore<T1, T2, TPlugOptions> Current
        {
            get => ValueTweener.Current;
        }

        public abstract T1 Value { get; set; }

        protected virtual void Awake()
        {
            ValueTweener.Getter = () => Value;

            ValueTweener.Setter = (value) => Value = value;
        }

        public void SetEase(int value)
        {
            Ease = (Ease)value;
        }

        public virtual TweenerCore<T1, T2, TPlugOptions> Tween(T2 endValue, float duration = -1f)
        {
            return ValueTweener.Tween(endValue, duration);
        }
    }
}