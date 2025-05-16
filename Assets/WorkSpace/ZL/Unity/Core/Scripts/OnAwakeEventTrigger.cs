using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity
{
    [AddComponentMenu("ZL/On Awake Event Trigger")]

    public sealed class OnAwakeEventTrigger : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private UnityEvent onAwakeEvent = null;

        public UnityEvent OnAwakeEvent
        {
            get => onAwakeEvent;
        }

        private void Awake()
        {
            onAwakeEvent.Invoke();

            Destroy(this);
        }
    }
}