using UnityEngine;

using ZL.Unity.IO;

using ZL.Unity.Singleton;

namespace ZL.Unity
{
    [AddComponentMenu("ZL/Application Manager (Singleton)")]

    public sealed class ApplicationManager : MonoSingleton<ApplicationManager>
    {
        [Space]

        [SerializeField]

        private BoolPref runInBackgroundPref = new("Run In Background", false);

        public BoolPref RunInBackgroundPref
        {
            get => runInBackgroundPref;
        }

        [Space]

        [SerializeField]

        private IntPref targetFrameRatePref = new("Target Frame Rate", 60);

        public IntPref TargetFrameRatePref
        {
            get => targetFrameRatePref;
        }

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Text("Cursor")]

        [AddIndent]

        [Alias("Visible")]

        private bool cursorVisible = true;

        [UsingCustomProperty]

        [AddIndent]

        [Alias("Lock State")]

        [SerializeField]

        private CursorLockMode cursorLockState = CursorLockMode.None;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [PropertyField]

        [Button(nameof(Pause))]

        [Button(nameof(Resume))]

        [Button(nameof(Quit))]

        private float timeScale = 1f;

        private void OnValidate()
        {
            if (Application.isPlaying == false)
            {
                return;
            }

            Cursor.visible = cursorVisible;

            Cursor.lockState = cursorLockState;

            TimeEx.TimeScale = timeScale;
        }

        protected override void Awake()
        {
            base.Awake();

            runInBackgroundPref.OnValueChanged += (value) =>
            {
                Application.runInBackground = value;
            };

            runInBackgroundPref.TryLoadValue();

            targetFrameRatePref.OnValueChanged += (value) =>
            {
                Application.targetFrameRate = value;
            };

            targetFrameRatePref.TryLoadValue();

            Cursor.visible = cursorVisible;

            Cursor.lockState = cursorLockState;

            TimeEx.TimeScale = timeScale;
        }

        public void Pause()
        {
            TimeEx.Pause();
        }

        public void Resume()
        {
            TimeEx.Resume();
        }

        public void Quit()
        {
            FixedApplication.Quit();
        }
    }
}