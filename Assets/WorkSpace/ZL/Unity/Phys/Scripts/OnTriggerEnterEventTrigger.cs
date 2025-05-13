using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity.Phys
{
    [AddComponentMenu("ZL/Phys/On Trigger Enter Event Trigger")]

    [DisallowMultipleComponent]

    public sealed class OnTriggerEnterEventTrigger : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private UnityEvent<Collider> onTriggerEnterEvent;

        private void OnTriggerEnter(Collider other)
        {
            onTriggerEnterEvent.Invoke(other);
        }
    }
}