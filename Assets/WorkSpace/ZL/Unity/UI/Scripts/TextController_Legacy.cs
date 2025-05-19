using UnityEngine;

using UnityEngine.UI;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Text Controller (Legacy)")]

    public sealed class TextController_Legacy : TextController
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        private Text text = null;

        public override string Text
        {
            get => text.text;

            set => text.text = value;
        }
    }
}