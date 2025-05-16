using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity
{
    public abstract class OnStopEventTrigger : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private UnityEvent onStopEvent = null;

        public UnityEvent OnStopEvent
        {
            get => onStopEvent;
        }

        protected abstract bool IsStoped { get; }

        private void LateUpdate()
        {
            if (IsStoped == true)
            {
                onStopEvent.Invoke();
            }
        }
    }
}