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
            get => valueTweener.Duration;

            set => valueTweener.Duration = value;
        }

        public float Delay
        {
            get => valueTweener.Delay;

            set => valueTweener.Delay = value;
        }

        public Ease Ease
        {
            get => valueTweener.Ease;

            set => valueTweener.Ease = value;
        }

        public bool IsIndependentUpdate
        {
            get => valueTweener.IsIndependentUpdate;

            set => valueTweener.IsIndependentUpdate = value;
        }

        public int LoopCount
        {
            get => valueTweener.LoopCount;

            set => valueTweener.LoopCount = value;
        }

        public LoopType LoopType
        {
            get => valueTweener.LoopType;

            set => valueTweener.LoopType = value;
        }

        public TweenerCore<T1, T2, TPlugOptions> Current
        {
            get => valueTweener.Current;
        }

        public abstract T1 Value { get; set; }

        protected virtual void Awake()
        {
            valueTweener.Getter = () => Value;

            valueTweener.Setter = (value) => Value = value;
        }

        public void SetEase(int value)
        {
            Ease = (Ease)value;
        }

        public virtual TweenerCore<T1, T2, TPlugOptions> Tween(T2 endValue, float duration = -1f)
        {
            return valueTweener.Tween(endValue, duration);
        }
    }
}