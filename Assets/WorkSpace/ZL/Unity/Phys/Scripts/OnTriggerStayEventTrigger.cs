using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity.Phys
{
    [AddComponentMenu("ZL/Phys/On Trigger Stay Event Trigger")]

    [DisallowMultipleComponent]

    public sealed class OnTriggerStayEventTrigger : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private UnityEvent<Collider> onTriggerStayEvent;

        private void OnTriggerStay(Collider other)
        {
            onTriggerStayEvent.Invoke(other);
        }
    }
}