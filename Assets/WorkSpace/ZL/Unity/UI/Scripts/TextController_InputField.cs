using UnityEngine;

using UnityEngine.UI;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Text Controller (Legacy Input Field)")]

    public sealed class TextController_InputField : TextController
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        [Alias("Text (UI)")]

        private InputField textUI = null;

        public override string Text
        {
            get => textUI.text;

            set => textUI.text = value;
        }
    }
}