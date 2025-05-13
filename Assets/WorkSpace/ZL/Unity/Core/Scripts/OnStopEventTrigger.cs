using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity
{
    public abstract class OnStopEventTrigger<TComponent> : MonoBehaviour

        where TComponent : Component
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [ReadOnly(true)]

        protected TComponent target;

        public TComponent Target
        {
            get => target;
        }

        [Space]

        [SerializeField]

        private UnityEvent onStopEvent;

        public UnityEvent OnStopEvent
        {
            get => onStopEvent;
        }

        public abstract bool IsStoped { get; }

        private void LateUpdate()
        {
            if (IsStoped == true)
            {
                onStopEvent.Invoke();
            }
        }
    }
}