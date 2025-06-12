using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity
{
    [AddComponentMenu("ZL/On Awake Event Trigger")]

    [DefaultExecutionOrder((int)ScriptExecutionOrder.Lazy)]

    public sealed class OnAwakeEventTrigger : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private UnityEvent onAwakeEvent = null;

        private void Awake()
        {
            onAwakeEvent.Invoke();

            enabled = false;
        }
    }
}