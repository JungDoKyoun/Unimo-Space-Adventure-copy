using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity
{
    [AddComponentMenu("ZL/On Disable Event Trigger")]

    public class OnDisableEventTrigger : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private UnityEvent onDisableEvent = null;

        public UnityEvent OnDisableEvent
        {
            get => onDisableEvent;
        }

        protected virtual void OnDisable()
        {
            onDisableEvent.Invoke();
        }
    }
}