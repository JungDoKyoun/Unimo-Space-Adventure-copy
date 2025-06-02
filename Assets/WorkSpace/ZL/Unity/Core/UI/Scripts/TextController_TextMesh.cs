using UnityEngine;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Text Controller (Text Mesh)")]

    public sealed class TextController_TextMesh : TextController
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        [Alias("Text (UI)")]

        private TextMesh textUI = null;

        public override string text
        {
            get => textUI.text;

            set => textUI.text = value;
        }
    }
}