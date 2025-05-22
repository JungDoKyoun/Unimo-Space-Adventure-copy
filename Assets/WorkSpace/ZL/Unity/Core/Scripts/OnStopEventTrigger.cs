using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity
{
    public abstract class OnStopEventTrigger : MonoBehaviour
    {
        public abstract UnityEvent OnStopEvent { get; }

        protected abstract bool IsStoped { get; }

        private void LateUpdate()
        {
            if (IsStoped == true)
            {
                OnStopEvent.Invoke();
            }
        }
    }
}