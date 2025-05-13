using UnityEngine;

using UnityEngine.Events;

using UnityEngine.UI;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Toggle Event Trigger")]

    [DisallowMultipleComponent]

    [RequireComponent(typeof(Toggle))]  

    public sealed class ToggleEventTrigger : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [ReadOnly(true)]

        private Toggle toggle;

        [Space]

        [SerializeField]

        private UnityEvent OnToggleOnEvent;

        [Space]

        [SerializeField]

        private UnityEvent OnToggleOffEvent;

        private void Awake()
        {
            toggle.onValueChanged.AddListener(OnToggleSwitched);
        }

        private void OnToggleSwitched(bool value)
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