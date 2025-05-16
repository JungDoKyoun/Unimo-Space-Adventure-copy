using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity.Phys
{
    [AddComponentMenu("ZL/Phys/On Trigger Exit Event Trigger")]

    public sealed class OnTriggerExitEventTrigger : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private UnityEvent<Collider> onTriggerExitEvent = null;

        private void OnTriggerExit(Collider other)
        {
            onTriggerExitEvent.Invoke(other);
        }
    }
}