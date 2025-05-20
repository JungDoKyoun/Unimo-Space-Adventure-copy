using UnityEngine;

using UnityEngine.Events;

using ZL.Unity.Collections;

namespace ZL.Unity
{
    [AddComponentMenu("ZL/Event Handler")]

    public sealed class EventHandler : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private SerializableDictionary<string, UnityEvent> events = null;

        public void Invoke(string key)
        {
            events[key].Invoke();
        }
    }
}