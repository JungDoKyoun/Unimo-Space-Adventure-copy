using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity.Phys
{
    [AddComponentMenu("ZL/Phys/On Collision Stay 2D Event Trigger")]

    public sealed class OnCollisionStay2DEventTrigger : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private UnityEvent<Collision2D> onCollisionStay2DEvent = null;

        private void OnCollisionStay2D(Collision2D collision)
        {
            onCollisionStay2DEvent.Invoke(collision);
        }
    }
}