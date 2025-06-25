using UnityEngine;

using UnityEngine.Events;

using ZL.Unity.Collections;

namespace ZL.Unity
{
    [AddComponentMenu("ZL/Event Handler (Void)")]

    public sealed class EventHandler_Void : MonoBehaviour
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