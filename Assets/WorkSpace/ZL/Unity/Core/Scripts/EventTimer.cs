using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity
{
    [AddComponentMenu("ZL/Event Timer")]

    public sealed class EventTimer : MonoBehaviour
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

        private UnityEvent onTimeEvent = null;

        public UnityEvent OnTimeEvent
        {
            get => onTimeEvent;
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