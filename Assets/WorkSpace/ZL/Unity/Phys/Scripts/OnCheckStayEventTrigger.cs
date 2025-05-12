using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity.Phys
{
    [AddComponentMenu("ZL/Phys/On Check Stay Event Trigger")]

    [DisallowMultipleComponent]

    public sealed class OnCheckStayEventTrigger : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [ReadOnly(true)]

        private ColliderChecker colliderChecker;

        [Space]

        [SerializeField]

        private UnityEvent onStayEvent;

        public UnityEvent OnStayEvent => onStayEvent;

        private void FixedUpdate()
        {
            if (colliderChecker.Check() == true)
            {
                onStayEvent.Invoke();
            }
        }
    }
}