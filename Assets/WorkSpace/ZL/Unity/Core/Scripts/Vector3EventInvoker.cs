using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity
{
    [AddComponentMenu("ZL/Vector3 Event Invoker")]

    public sealed class Vector3EventInvoker : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private Vector3 vector = Vector3.zero;

        public Vector3 Vector
        {
            get => vector;

            set => vector = value;
        }

        public float VectorX
        {
            set => vector.x = value;
        }

        public float VectorY
        {
            set => vector.y = value;
        }

        public float VectorZ
        {
            set => vector.z = value;
        }

        [Space]

        [SerializeField]

        private UnityEvent<Vector3> @event;

        public void Invoke()
        {
            @event.Invoke(vector);
        }
    }
}