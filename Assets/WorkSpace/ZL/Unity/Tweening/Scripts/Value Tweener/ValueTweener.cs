using DG.Tweening;

using DG.Tweening.Core;

using DG.Tweening.Plugins.Options;

using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity.Tweening
{
    public abstract class ValueTweener<T1, T2, TPlugOptions>

        where TPlugOptions : struct, IPlugOptions
    {
        [SerializeField]

        private T2 endValue = default;

        [SerializeField]

        private float duration = 0f;

        [SerializeField]

        private float delay = 0f;

        [SerializeField]

        private Ease ease = Ease.Linear;

        [SerializeField]

        private bool isIndependentUpdate = true;

        [SerializeField]

        [Tooltip("1 = Loop once (Default)\n-1 = Infinity loop")]

        private int loops = 1;

        [SerializeField]

        [UsingCustomProperty]

        [ToggleIf(nameof(loops), 0, true)]

        [ToggleIf(nameof(loops), 1, true)]

        [AddIndent]

        [PropertyField]

        private LoopType loopType = LoopType.Restart;

        [Space]

        [SerializeField]

        private UnityEvent onStartEvent = new UnityEvent();

        public UnityEvent OnStartEvent
        {
            get => onStartEvent;
        }

        [Space]

        [SerializeField]

        private UnityEvent onCompleteEvent = new UnityEvent();

        public UnityEvent OnCompleteEvent
        {
            get => onCompleteEvent;
        }

        protected DOGetter<T1> getter = null;

        public DOGetter<T1> Getter
        {
            get => getter;

            set => getter = value;
        }

        protected DOSetter<T1> setter = null;

        public DOSetter<T1> Setter
        {
            get => setter;

            set => setter = value;
        }

        public TweenerCore<T1, T2, TPlugOptions> Current { get; private set; } = null;

        public void SetEndValue(T2 endValue)
        {
            this.endValue = endValue;
        }

        public void SetDuration(float duration)
        {
            this.duration = duration;
        }

        public void SetDelay(float delay)
        {
            this.delay = delay;
        }

        public void SetEase(int ease)
        {
            SetEase((Ease)ease);
        }

        public void SetEase(Ease ease)
        {
            this.ease = ease;
        }

        public void SetLoops(int loops)
        {
            this.loops = loops;
        }

        public void SetLoopType(int loopType)
        {
            SetLoopType((LoopType)loopType);
        }

        public void SetLoopType(LoopType loopType)
        {
            this.loopType = loopType;
        }

        public virtual void Play()
        {
            Current.Kill();

            Current = To(getter, setter, endValue, duration);

            if (delay != 0f)
            {
                Current.SetDelay(delay);
            }

            if (ease != Ease.Linear)
            {
                Current.SetEase(ease);
            }

            if (isIndependentUpdate == true)
            {
                Current.SetUpdate(isIndependentUpdate);
            }

            if (loops != 1)
            {
                Current.SetLoops(loops, loopType);
            }

            if (onStartEvent.GetPersistentEventCount() != 0)
            {
                Current.OnStart(onStartEvent.Invoke);
            }

            if (onCompleteEvent.GetPersistentEventCount() != 0)
            {
                Current.OnComplete(onCompleteEvent.Invoke);
            }

            Current.SetAutoKill(false);

            Current.SetRecyclable(true);

            Current.Restart();
        }

        protected abstract TweenerCore<T1, T2, TPlugOptions> To(DOGetter<T1> getter, DOSetter<T1> setter, in T2 endValue, float duration);
    }
}