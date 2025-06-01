using System;

using System.Collections;

using UnityEngine;

using ZL.Unity.Coroutines;

using ZL.Unity.UI;

namespace ZL.Unity
{
    [AddComponentMenu("ZL/Clock")]

    //[ExecuteInEditMode]

    public sealed class Clock : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponentInChildren]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private TextController timeStampText = null;

        [Space]

        [SerializeField]

        private int hour = 0;

        public int Hour
        {
            get => hour;

            set
            {
                hour = value;

                Refresh();
            }
        }

        [SerializeField]

        private int minute = 0;

        public int Minute
        {
            get => minute;

            set
            {
                minute = value;

                Refresh();
            }
        }

        [SerializeField]

        private int seconds = 0;

        public int Seconds
        {
            get => seconds;

            set
            {
                seconds = value;

                Refresh();
            }
        }

        [Space]

        [SerializeField]

        private float timeSpeed = 0f;

        [Space]

        [SerializeField]

        private bool isBlinking = true;

        public bool IsBlinking
        {
            set
            {
                isBlinking = value;

                if (isBlinking == true)
                {
                    StartBlinking();
                }

                else
                {
                    StopBlinking();
                }
            }
        }

        [SerializeField]

        [UsingCustomProperty]

        [ToggleIf(nameof(isBlinking), false)]

        private bool syncBlinking = false;

        [Space]

        [SerializeField]

        [Tooltip("{0} = Hour\n{1} = Minute\n{2} = Seconds")]

        private string timeStampFormat = "{0:D2}:{1:D2}:{2:D2}";

        [SerializeField]

        [Tooltip("{0} = Hour\n{1} = Minute\n{2} = Seconds")]

        [UsingCustomProperty]

        [ToggleIf(nameof(isBlinking), false)]

        [Alias("Time Stamp Format (Blinked)")]

        private string timeStampFormat_Blinked = "{0:D2} {1:D2} {2:D2}";

        private bool isBlinked = false;

        private TimeSpan timeSpan = TimeSpan.Zero;

        private void OnValidate()
        {
            Refresh();
        }

        private void OnEnable()
        {
            IsBlinking = isBlinking;
        }

        private void Update()
        {
            Refresh();

            #if UNITY_EDITOR

            if (Application.isPlaying == false)
            {
                return;
            }

            #endif

            timeSpan += TimeSpan.FromSeconds(timeSpeed * Time.deltaTime);
        }

        public void Refresh()
        {
            timeSpan = new TimeSpan(hour, minute, seconds);

            hour = timeSpan.Hours;

            minute = timeSpan.Minutes;

            seconds = timeSpan.Seconds;

            if (timeStampText == null)
            {
                return;
            }

            if (isBlinked == false)
            {
                timeStampText.text = string.Format(timeStampFormat, hour, minute, (int)seconds);
            }

            else
            {
                timeStampText.text = string.Format(timeStampFormat_Blinked, hour, minute, (int)seconds);
            }
        }

        public string GetTimeStamp()
        {
            return string.Format(timeStampFormat, hour, minute, seconds);
        }

        public void StartBlinking()
        {
            if (blinkingRoutine != null)
            {
                return;
            }

            blinkingRoutine = BlinkingRoutine();

            StartCoroutine(blinkingRoutine);
        }

        public void StopBlinking()
        {
            if (blinkingRoutine == null)
            {
                return;
            }

            StopCoroutine(blinkingRoutine);

            blinkingRoutine = null;
        }

        private IEnumerator blinkingRoutine = null;

        private IEnumerator BlinkingRoutine()
        {
            while (true)
            {
                if (syncBlinking == true)
                {
                    if (timeSpan.TotalSeconds < 0.5)
                    {
                        isBlinked = false;
                    }

                    else
                    {
                        isBlinked = true;
                    }

                    yield return null;
                }

                else
                {
                    isBlinked = false;

                    yield return WaitForSecondsCache.Get(0.5f);

                    isBlinked = true;

                    yield return WaitForSecondsCache.Get(0.5f);
                }
            }
        }
    }
}