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
        #region Ease Tooltip

        private const string easeTooltip =

            "Unset = 0,\n" +
            "Linear = 1,\n" +
            "In Sine = 2,\n" +
            "Out Sine = 3,\n" +
            "In Out Sine = 4,\n" +
            "In Quad = 5,\n" +
            "Out Quad = 6,\n" +
            "In Out Quad = 7,\n" +
            "In Cubic = 8,\n" +
            "Out Cubic = 9,\n" +
            "In Out Cubic = 10,\n" +
            "In Quart = 11,\n" +
            "Out Quart = 12,\n" +
            "In Out Quart = 13,\n" +
            "In Quint = 14,\n" +
            "Out Quint = 15,\n" +
            "In Out Quint = 16,\n" +
            "In Expo = 17,\n" +
            "Out Expo = 18,\n" +
            "In Out Expo = 19,\n" +
            "In Circ = 20,\n" +
            "Out Circ = 21,\n" +
            "In Out Circ = 22,\n" +
            "In Elastic = 23,\n" +
            "Out Elastic = 24,\n" +
            "In Out Elastic = 25,\n" +
            "In Back = 26,\n" +
            "Out Back = 27,\n" +
            "In Out Back = 28,\n" +
            "In Bounce = 29,\n" +
            "Out Bounce = 30,\n" +
            "In Out Bounce = 31,\n" +
            "Flash = 32,\n" +
            "In Flash = 33,\n" +
            "Out Flash = 34,\n" +
            "In Out Flash = 35,\n" +
            "INTERNAL_Zero = 36,\n" +
            "INTERNAL_Custom = 37";

        #endregion

        [SerializeField]

        private T2 endValue = default;

        [SerializeField]

        private float duration = 0f;

        [SerializeField]

        private float delay = 0f;

        [SerializeField]

        [Tooltip(easeTooltip)]

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