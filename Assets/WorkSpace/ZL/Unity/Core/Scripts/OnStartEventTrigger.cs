using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity
{
    [AddComponentMenu("ZL/On Start Event Trigger")]

    [DisallowMultipleComponent]

    public sealed class OnStartEventTrigger : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private UnityEvent onStartEvent;

        private void Start()
        {
            onStartEvent.Invoke();

            Destroy(this);
        }
    }
}