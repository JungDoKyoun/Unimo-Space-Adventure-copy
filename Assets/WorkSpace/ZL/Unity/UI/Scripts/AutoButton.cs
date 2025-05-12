using System.Collections;

using UnityEngine;

using UnityEngine.EventSystems;

using UnityEngine.UI;

using ZL.Unity.Coroutines;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Auto Button")]

    public sealed class AutoButton : Button
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Alias("Threshold")]

        private float autoClickThreshold = 0.5f;

        [SerializeField]

        [UsingCustomProperty]

        [Text("Interval")]

        [AddIndent]

        [Alias("Use Curve")]

        private bool useAutoClickIntervalCurve = false;

        [SerializeField]

        [UsingCustomProperty]

        [ToggleIf(nameof(useAutoClickIntervalCurve), true)]

        [AddIndent]

        [Alias("")]

        private float autoClickInterval = 0.1f;

        [SerializeField]

        [UsingCustomProperty]

        [ToggleIf(nameof(useAutoClickIntervalCurve), false)]

        [AddIndent]

        [Alias("")]

        private AnimationCurve autoClickIntervalCurve;

        private float pressedTime = 0f;

        #if UNITY_EDITOR

        protected override void Reset()
        {
            base.Reset();

        #else

        private void Reset()
        {

        #endif

            var keyframes = new Keyframe[]
            {
                new Keyframe(0f, 1f),

                new Keyframe(3f, 0.1f),
            };

            autoClickIntervalCurve = new AnimationCurve(keyframes);

            FixedAnimationUtility.SetKeyRightTangentMode(autoClickIntervalCurve, 0, FixedTangentMode.Auto);

            FixedAnimationUtility.SetKeyLeftTangentMode(autoClickIntervalCurve, 1, FixedTangentMode.Auto);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (interactable == false)
            {
                return;
            }

            StartAutoClick();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            StopAutoClick();
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }

            if (IsActive() == false || IsInteractable() == false)
            {
                return;
            }

            if (pressedTime > 0f || autoClickThreshold == 0f)
            {
                pressedTime = 0f;
            }

            UISystemProfilerApi.AddMarker("Button.onClick", this);

            onClick.Invoke();
        }

        private void StartAutoClick()
        {
            if (autoClickRoutine != null)
            {
                return;
            }

            autoClickRoutine = AutoClickRoutine();

            StartCoroutine(autoClickRoutine);
        }

        private void StopAutoClick()
        {
            if (autoClickRoutine == null)
            {
                return;
            }

            StopCoroutine(autoClickRoutine);

            autoClickRoutine = null;
        }

        private IEnumerator autoClickRoutine = null;

        private IEnumerator AutoClickRoutine()
        {
            pressedTime = 0f;

            float interval = autoClickThreshold;

            while (true)
            {
                yield return WaitForInterval(interval);

                if (useAutoClickIntervalCurve == true)
                {
                    interval = autoClickIntervalCurve.Evaluate(pressedTime);
                }

                else
                {
                    interval = autoClickInterval;
                }
            }
        }

        private IEnumerator WaitForInterval(float seconds)
        {
            yield return WaitForSecondsCache.Get(seconds);

            pressedTime += seconds;

            onClick.Invoke();
        }
    }
}