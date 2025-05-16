using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity
{
    [AddComponentMenu("ZL/On Enable Event Trigger")]

    public sealed class OnEnableEventTrigger : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private UnityEvent onEnableEvent = null;

        public UnityEvent OnEnableEvent
        {
            get => onEnableEvent;
        }

        private void OnEnable()
        {
            onEnableEvent.Invoke();
        }
    }
}