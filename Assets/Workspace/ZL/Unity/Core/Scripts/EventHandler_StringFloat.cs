using UnityEngine;

using UnityEngine.Events;

using ZL.Unity.Collections;

namespace ZL.Unity
{
    [AddComponentMenu("ZL/Event Handler (String, Float)")]

    public sealed class EventHandler_StringFloat : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private float param = 0f;

        public float Param
        {
            set => param = value;
        }

        [Space]

        [SerializeField]

        private SerializableDictionary<string, UnityEvent<string, float>> events = null;

        public void Invoke(string key)
        {
            events[key].Invoke(key, param);
        }
    }
}