using UnityEngine;

using UnityEngine.Events;

using UnityEngine.UI;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Toggle Event Trigger")]

    public sealed class ToggleEventTrigger : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        private Toggle toggle = null;

        [Space]

        [SerializeField]

        private UnityEvent OnToggleOnEvent = null;

        [Space]

        [SerializeField]

        private UnityEvent OnToggleOffEvent = null;

        private void Awake()
        {
            toggle.onValueChanged.AddListener(OnToggleChanged);
        }

        private void OnToggleChanged(bool value)
        {
            if (value == true)
            {
                OnToggleOnEvent.Invoke();
            }

            else
            {
                OnToggleOffEvent.Invoke();
            }
        }
    }
}