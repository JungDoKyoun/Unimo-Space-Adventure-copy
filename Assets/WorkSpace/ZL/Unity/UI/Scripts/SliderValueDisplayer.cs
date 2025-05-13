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

        private Slider slider;

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private TextController textController;

        #if UNITY_EDITOR

        private float sliderValue = 0f;

        private string inputText = null;

        private void Reset()
        {
            this.DisallowMultiple();
        }

        private void Awake()
        {
            if (slider != null)
            {
                sliderValue = slider.value;
            }

            if (textController != null)
            {
                inputText = textController.Text;
            }
        }

        private void Update()
        {
            if (Application.isPlaying == true)
            {
                return;
            }

            if (textController != null && slider != null)
            {
                if (sliderValue != slider.value)
                {
                    textController.SetText(slider.value);
                }

                else if (inputText != textController.Text)
                {
                    TrySetValue(textController.Text);
                }

                inputText = textController.Text;

                sliderValue = slider.value;
            }
        }

#endif

        public void TrySetValue(string text)
        {
            if (float.TryParse(text, out float value) == true)
            {
                slider.value = value;
            }
        }
    }
}