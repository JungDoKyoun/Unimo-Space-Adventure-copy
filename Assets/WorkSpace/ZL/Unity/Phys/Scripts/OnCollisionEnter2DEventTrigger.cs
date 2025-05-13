using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity.Phys
{
    [AddComponentMenu("ZL/Phys/On Collision Enter 2D Event Trigger")]

    [DisallowMultipleComponent]

    public sealed class OnCollisionEnter2DEventTrigger : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private UnityEvent<Collision2D> onCollisionEnter2DEvent;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            onCollisionEnter2DEvent.Invoke(collision);
        }
    }
}