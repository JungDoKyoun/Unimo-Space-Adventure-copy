using System.Collections;

using UnityEngine;

using ZL.Unity.Coroutines;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Digital Clock")]

    [DisallowMultipleComponent]

    public sealed class DigitalClock : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private TextController textController;

        [Space]

        [SerializeField]

        private float timeSpeed = 0f;

        [SerializeField]

        private bool syncBlinking = false;

        [Space]

        [SerializeField]

        private int hour = 0;

        public int Hour
        {
            get => hour;

            set
            {
                hour = value;

                if (hour > 23)
                {
                    hour = 0;
                }

                else if (hour < 0)
                {
                    hour = 59;
                }
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

                if (minute >= 60)
                {
                    Hour += minute / 60;

                    minute %= 60;
                }

                else if (minute < 0)
                {
                    Hour += minute / 60;

                    minute %= 60;
                }
            }
        }

        [SerializeField]

        private float seconds = 0f;

        public float Seconds
        {
            get => seconds;

            set
            {
                seconds = value;

                if (seconds >= 60f)
                {
                    Minute += Mathf.FloorToInt(seconds / 60f);

                    seconds %= 60f;
                }

                else if (seconds < 0f)
                {
                    Minute += Mathf.FloorToInt(seconds / 60f);

                    seconds %= 60f;
                }
            }
        }

        private bool isBlinked = false;

        private void OnValidate()
        {
            Seconds = seconds;

            Minute = minute;

            Hour = hour;

            if (textController != null)
            {
                textController.Text = $"{hour:D2}:{minute:D2}";
            }
        }

        private void OnEnable()
        {
            StartCoroutine(Blinking());
        }

        private void Update()
        {
            if (isBlinked == false)
            {
                textController.Text = $"{hour:D2}:{minute:D2}";
            }

            else
            {
                textController.Text = $"{hour:D2} {minute:D2}";
            }

            Seconds += Time.deltaTime * timeSpeed;
        }

        private IEnumerator Blinking()
        {
            while (true)
            {
                if (syncBlinking == true)
                {
                    if (seconds % 1 < 0.5f)
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

                    yield return new WaitForSeconds(0.5f);

                    isBlinked = true;

                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
    }
}