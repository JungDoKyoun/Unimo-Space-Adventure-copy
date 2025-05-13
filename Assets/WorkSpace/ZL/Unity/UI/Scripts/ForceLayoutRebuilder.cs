using UnityEngine;

using UnityEngine.UI;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Force Layout Rebuilder")]

    [DisallowMultipleComponent]

    public sealed class ForceLayoutRebuilder : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [ReadOnly(true)]

        [PropertyField]

        [ReadOnly(false)]

        [Button(nameof(ForceRebuildLayout))]

        private RectTransform rectTransform;

        private void Start()
        {
            ForceRebuildLayout();

            enabled = false;
        }

        public void ForceRebuildLayout()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        }
    }
}