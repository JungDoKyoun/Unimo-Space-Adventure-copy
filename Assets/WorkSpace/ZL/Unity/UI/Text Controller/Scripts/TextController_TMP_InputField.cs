using TMPro;

using UnityEngine;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Text Controller (TextMeshPro - Input Field)")]

    [RequireComponent(typeof(TextController))]

    public sealed class TextController_TMP_InputField : ComponentController<TMP_InputField>, ITextController
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