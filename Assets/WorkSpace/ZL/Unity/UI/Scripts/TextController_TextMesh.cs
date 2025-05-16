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

        private TextMesh text = null;

        public override string Text
        {
            get => text.text;

            set => text.text = value;
        }
    }
}