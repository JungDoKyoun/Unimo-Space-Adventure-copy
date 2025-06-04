using UnityEngine;

using UnityEngine.Events;

using ZL.Unity.Collections;

namespace ZL.Unity
{
    [AddComponentMenu("ZL/Vector3 Event Hanlder")]

    public sealed class Vector3EventHandler : MonoBehaviour
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

        private SerializableDictionary<string, UnityEvent<Vector3>> events = null;

        public void Invoke(string key)
        {
            events[key].Invoke(vector);
        }
    }
}