using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity.Phys
{
    [AddComponentMenu("ZL/Phys/On Collision Exit 2D Event Trigger")]

    [DisallowMultipleComponent]

    public sealed class OnCollisionExit2DEventTrigger : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private UnityEvent<Collision2D> onCollisionExit2DEvent;

        private void OnCollisionExit2D(Collision2D collision)
        {
            onCollisionExit2DEvent.Invoke(collision);
        }
    }
}