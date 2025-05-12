using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity.Phys
{
    [AddComponentMenu("ZL/Phys/On Collision Enter Event Trigger")]

    [DisallowMultipleComponent]

    public sealed class OnCollisionEnterEventTrigger : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private UnityEvent<Collision> onCollisionEnterEvent;

        private void OnCollisionEnter(Collision collision)
        {
            onCollisionEnterEvent.Invoke(collision);
        }
    }
}