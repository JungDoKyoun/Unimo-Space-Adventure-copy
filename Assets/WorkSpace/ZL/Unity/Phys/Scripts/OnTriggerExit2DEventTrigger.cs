using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity.Phys
{
    [AddComponentMenu("ZL/Phys/On Trigger Exit 2D Event Trigger")]

    [DisallowMultipleComponent]

    public sealed class OnTriggerExit2DEventTrigger : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private UnityEvent<Collider2D> onTriggerExit2DEvent;

        private void OnTriggerExit2D(Collider2D other)
        {
            onTriggerExit2DEvent.Invoke(other);
        }
    }
}