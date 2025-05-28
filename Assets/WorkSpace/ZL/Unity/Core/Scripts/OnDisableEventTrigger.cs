using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity
{
    [AddComponentMenu("ZL/On Disable Event Trigger")]

    public sealed class OnDisableEventTrigger : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private UnityEvent onDisableEvent = null;

        public UnityEvent OnDisableEvent
        {
            get => onDisableEvent;
        }

        private void OnDisable()
        {
            onDisableEvent.Invoke();
        }
    }
}