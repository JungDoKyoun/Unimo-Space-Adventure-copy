using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity.Phys
{
    [AddComponentMenu("ZL/Phys/On Trigger Enter 2D Event Trigger")]

    [DisallowMultipleComponent]

    public sealed class OnTriggerEnter2DEventTrigger : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private UnityEvent<Collider2D> onTriggerEnter2DEvent;

        private void OnTriggerEnter2D(Collider2D other)
        {
            onTriggerEnter2DEvent.Invoke(other);
        }
    }
}