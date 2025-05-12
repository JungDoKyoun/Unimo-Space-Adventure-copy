using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity.Phys
{
    [AddComponentMenu("ZL/Phys/On Trigger Stay 2D Event Trigger")]

    [DisallowMultipleComponent]

    public sealed class OnTriggerStay2DEventTrigger : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private UnityEvent<Collider2D> onTriggerStay2DEvent;

        private void OnTriggerStay2D(Collider2D other)
        {
            onTriggerStay2DEvent.Invoke(other);
        }
    }
}