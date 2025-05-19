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

        private TMP_InputField text = null;

        public override string Text
        {
            get => text.text;

            set => text.text = value;
        }
    }
}