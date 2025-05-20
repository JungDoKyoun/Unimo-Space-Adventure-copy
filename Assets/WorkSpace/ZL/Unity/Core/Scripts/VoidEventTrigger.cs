using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity
{
    [AddComponentMenu("ZL/Void Event Invoker")]

    public sealed class VoidEventInvoker : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private UnityEvent[] events = null;

        public void Invoke(int index)
        {
            events[index].Invoke();
        }
    }
}