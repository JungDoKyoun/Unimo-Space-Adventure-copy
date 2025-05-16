using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity.Phys
{
    [AddComponentMenu("ZL/Phys/On Collision Exit Event Trigger")]

    public sealed class OnCollisionExitEventTrigger : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private UnityEvent<Collision> onCollisionExitEvent = null;

        private void OnCollisionExit(Collision collision)
        {
            onCollisionExitEvent.Invoke(collision);
        }
    }
}