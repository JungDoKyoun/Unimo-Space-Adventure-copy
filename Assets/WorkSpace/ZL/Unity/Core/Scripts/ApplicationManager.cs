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

        private BoolPref runInBackgroundPref = new BoolPref("Run In Background", false);

        public BoolPref RunInBackgroundPref
        {
            get => runInBackgroundPref;
        }

        [Space]

        [SerializeField]

        private IntPref targetFrameRatePref = new IntPref("Target Frame Rate", 60);

        public IntPref TargetFrameRatePref
        {
            get => targetFrameRatePref;
        }

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
            TimeEx.TimeScale = timeScale;
        }

        protected override void Awake()
        {
            base.Awake();

            runInBackgroundPref.OnValueChangedAction += (value) =>
            {
                Application.runInBackground = value;
            };

            runInBackgroundPref.TryLoadValue();

            targetFrameRatePref.OnValueChangedAction += (value) =>
            {
                Application.targetFrameRate = value;
            };

            targetFrameRatePref.TryLoadValue();
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