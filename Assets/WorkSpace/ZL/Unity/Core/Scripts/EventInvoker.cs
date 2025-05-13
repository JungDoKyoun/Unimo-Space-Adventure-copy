using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity
{
    [AddComponentMenu("ZL/Event Invoker")]

    [DisallowMultipleComponent]

    public sealed class EventInvoker : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private float time = 0f;

        public float Time
        {
            get => time;

            set => time = value;
        }

        [Space]

        [SerializeField]

        private UnityEvent onTimeEvent;

        public UnityEvent OnTimeEvent
        {
            get => onTimeEvent;
        }

        private void OnDisable()
        {
            CancelInvoke(nameof(OnTime));
        }

        public void Invoke()
        {
            Invoke(time);
        }

        public void Invoke(float time)
        {
            Invoke(nameof(OnTime), time);
        }

        private void OnTime()
        {
            onTimeEvent.Invoke();
        }
	}
}