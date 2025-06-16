using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity
{
    [AddComponentMenu("ZL/On Start Event Trigger")]

    public sealed class OnStartEventTrigger : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private UnityEvent onStartEvent = null;

        private void Start()
        {
            onStartEvent.Invoke();

            enabled = false;
        }
    }
}