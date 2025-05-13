using UnityEngine;

namespace ZL.Unity
{
    public static partial class FixedTime
    {
        public static bool IsPaused { get; private set; } = false;

        private static float timeScale;

        public static float TimeScale
        {
            get => timeScale;

            set
            {
                if (pauseCount != 0)
                {
                    timeScale = value;
                }

                else
                {
                    Time.timeScale = value;
                }
            }
        }

        private static int pauseCount = 0;

        static FixedTime()
        {
            timeScale = Time.timeScale;
        }

        public static void Pause()
        {
            if (pauseCount++ == 0)
            {
                timeScale = Time.timeScale;

                Time.timeScale = 0f;

                IsPaused = true;
            }
        }

        public static void Resume()
        {
            if (--pauseCount <= 0)
            {
                pauseCount = 0;

                Time.timeScale = timeScale;

                IsPaused = false;
            }
        }
    }
}