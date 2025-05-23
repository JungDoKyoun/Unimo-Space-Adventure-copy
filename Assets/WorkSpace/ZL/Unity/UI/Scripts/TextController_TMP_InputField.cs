using TMPro;

using UnityEngine;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Text Controller (TMP Input Field)")]

    public sealed class TextController_TMP_InputField : TextController
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        [Alias("Text (UI)")]

        private TMP_InputField textUI = null;

        public override string Text
        {
            get => textUI.text;

            set => textUI.text = value;
        }
    }
}