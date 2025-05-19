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

        private InputField text = null;

        public override string Text
        {
            get => text.text;

            set => text.text = value;
        }
    }
}