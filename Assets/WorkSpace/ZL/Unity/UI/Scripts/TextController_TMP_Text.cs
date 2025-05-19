using TMPro;

using UnityEngine;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Text Controller (TMP Text)")]

    public sealed class TextController_TMP_Text : TextController
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        private TMP_Text text = null;

        public override string Text
        {
            get => text.text;

            set => text.text = value;
        }
    }
}