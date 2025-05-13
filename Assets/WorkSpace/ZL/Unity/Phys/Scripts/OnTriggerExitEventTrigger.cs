using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity.Phys
{
    [AddComponentMenu("ZL/Phys/On Trigger Exit Event Trigger")]

    [DisallowMultipleComponent]

    public sealed class OnTriggerExitEventTrigger : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private UnityEvent<Collider> onTriggerExitEvent;

        private void OnTriggerExit(Collider other)
        {
            onTriggerExitEvent.Invoke(other);
        }
    }
}