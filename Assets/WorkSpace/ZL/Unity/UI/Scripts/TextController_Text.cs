using UnityEngine;

using UnityEngine.UI;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Text Controller (Legacy Text)")]

    public sealed class TextController_Text : TextController
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        [Alias("Text (UI)")]

        private Text textUI = null;

        public override string Text
        {
            get => textUI.text;

            set => textUI.text = value;
        }
    }
}