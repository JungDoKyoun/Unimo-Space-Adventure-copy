using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity.Phys
{
    [AddComponentMenu("ZL/Phys/On Check Enter Event Trigger")]

    [DisallowMultipleComponent]

    public sealed class OnCheckEnterEventTrigger : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [ReadOnly(true)]

        private ColliderChecker colliderChecker;

        [Space]

        [SerializeField]

        private UnityEvent onEnterEvent;

        public UnityEvent OnEnterEvent => onEnterEvent;

        [Space]

        [SerializeField]

        private UnityEvent onStayEvent;

        public UnityEvent OnStayEvent => onStayEvent;

        [Space]

        [SerializeField]

        private UnityEvent onExitEvent;

        public UnityEvent OnExitEvent => onExitEvent;

        private bool isChecked = false;

        private void FixedUpdate()
        {
            if (colliderChecker.Check() == true)
            {
                if (isChecked == false)
                {
                    isChecked = true;

                    onEnterEvent.Invoke();
                }

                onStayEvent.Invoke();
            }

            else if (isChecked == true)
            {
                isChecked = false;

                onExitEvent.Invoke();
            }
        }
    }
}