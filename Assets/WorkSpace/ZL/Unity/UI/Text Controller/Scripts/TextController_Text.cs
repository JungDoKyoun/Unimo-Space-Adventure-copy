using UnityEngine;

using UnityEngine.UI;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Text Controller (Text (Legacy))")]

    [RequireComponent(typeof(TextController))]

    public sealed class TextController_Text : ComponentController<Text>, ITextController
    {
        public string Text
        {
            get => Target.text;

            set => Target.text = value;
        }

        public void SetText(int value)
        {
            Target.text = value.ToString();
        }

        public void SetText(float value)
        {
            Target.text = value.ToString();
        }
    }
}