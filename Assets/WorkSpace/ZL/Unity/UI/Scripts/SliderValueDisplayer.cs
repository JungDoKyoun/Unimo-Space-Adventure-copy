using UnityEngine;

using UnityEngine.UI;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Slider Value Displayer")]

    [ExecuteInEditMode]

    public sealed class SliderValueDisplayer : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private Slider slider = null;

        public Slider Slider
        {
            get => slider;
        }

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        [Alias("Slider Value Text (UI)")]

        private TextController sliderValueTextUI = null;

        [Space]

        [SerializeField]

        private string format = "{0:F0}/{1:F0}";

        #if UNITY_EDITOR

        private float sliderMaxValue = 0f;

        private float sliderValue = 0f;

        private string sliderValueText = null;

        private void Awake()
        {
            sliderMaxValue = slider.maxValue;

            sliderValue = slider.value;

            sliderValueText = sliderValueTextUI.Text;
        }

        private void Update()
        {
            if (Application.isPlaying == true)
            {
                return;
            }

            if (slider != null && sliderValueTextUI != null)
            {
                if (sliderMaxValue != slider.maxValue)
                {
                    RefreshText();
                }

                else if (sliderValue != slider.value)
                {
                    RefreshText();
                }

                else if (sliderValueText != sliderValueTextUI.Text)
                {
                    TrySetValue(sliderValueTextUI.Text);
                }

                sliderMaxValue = slider.maxValue;

                sliderValue = slider.value;

                sliderValueText = sliderValueTextUI.Text;
            }
        }

        #endif

        public void RefreshText()
        {
            sliderValueTextUI.Text = string.Format(format, slider.value, slider.maxValue);
        }

        public void TrySetValue(string valueText)
        {
            if (float.TryParse(valueText, out float value) == true)
            {
                slider.value = value;
            }
        }
    }
}