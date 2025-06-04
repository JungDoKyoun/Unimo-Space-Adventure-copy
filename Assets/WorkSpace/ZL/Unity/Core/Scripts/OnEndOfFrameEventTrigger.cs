using System.Collections;

using UnityEngine;

using UnityEngine.Events;

using ZL.Unity.Coroutines;

namespace ZL.Unity
{
    [AddComponentMenu("ZL/On End Of Frame Event Trigger")]

    public sealed class OnEndOfFrameEventTrigger : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private UnityEvent onEndOfFrameEvent = null;

        public UnityEvent OnEndOfFrameEvent
        {
            get => onEndOfFrameEvent;
        }

        private void OnEnable()
        {
            StartCoroutine(OnEndOfFrameRoutine());
        }

        private IEnumerator OnEndOfFrameRoutine()
        {
            while (true)
            {
                yield return WaitForEndOfFrameCache.Get();

                Debug.Log(2);

                onEndOfFrameEvent.Invoke();
            }
        }
    }
}