using UnityEngine;

using ZL.Unity.IO;

using ZL.Unity.Singleton;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Frame Rate Displayer (Singleton)")]

    [DefaultExecutionOrder(-1)]

    public sealed class FrameRateDisplayer : MonoSingleton<FrameRateDisplayer>
    {
        [Space]

        [SerializeField]

        private TextController textController = null;

        [Space]

        [SerializeField]

        private string format = "{0:0.0} ms ({1:0} fps)";

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [PropertyField]

        [AddIndent(3)]

        [Button(nameof(Load))]

        [Button(nameof(Save))]

        private BoolPref displayFrameRatePref = new BoolPref("Display Frame Rate", true);

        private float time = 0f;

        private float ms;

        private float fps;

        #if UNITY_EDITOR

        private void OnValidate()
        {
            if (Application.isPlaying == true)
            {
                displayFrameRatePref.Value = displayFrameRatePref.Value;
            }
        }

        #endif

        protected override void Awake()
        {
            base.Awake();

            displayFrameRatePref.OnValueChangedAction += (value) =>
            {
                gameObject.SetActive(value);
            };

            displayFrameRatePref.TryLoadValue();
        }

        private void Update()
        {
            time += (Time.unscaledDeltaTime - time) * 0.1f;

            ms = 1000f * time;

            fps = 1f / time;

            textController.Text = string.Format(format, ms, fps);
        }

        public void Load()
        {
            displayFrameRatePref.LoadValue();
        }

        public void Save()
        {
            displayFrameRatePref.SaveValue();
        }
    }
}