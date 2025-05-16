using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity.Phys
{
    [AddComponentMenu("ZL/Phys/On Collision Enter Event Trigger")]

    public sealed class OnCollisionEnterEventTrigger : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private UnityEvent<Collision> onCollisionEnterEvent = null;

        private void OnCollisionEnter(Collision collision)
        {
            onCollisionEnterEvent.Invoke(collision);
        }
    }
}