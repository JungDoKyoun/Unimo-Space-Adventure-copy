using System.Collections;

#if UNITY_EDITOR

using UnityEditor;

using UnityEditor.UI;

#endif

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

        private AnimationCurve autoClickIntervalCurve = null;

        private float pressedTime = 0f;

        #if UNITY_EDITOR

        [CustomEditor(typeof(AutoButton))]

        public sealed class AutoButtonEditor : ButtonEditor
        {
            private AutoButton autoButton;

            private SerializedProperty autoClickThreshold;

            private SerializedProperty useAutoClickIntervalCurve;

            private SerializedProperty autoClickInterval;

            private SerializedProperty autoClickIntervalCurve;

            protected override void OnEnable()
            {
                base.OnEnable();

                autoButton = target as AutoButton;

                autoClickThreshold = serializedObject.FindProperty(nameof(autoButton.autoClickThreshold));

                useAutoClickIntervalCurve = serializedObject.FindProperty(nameof(autoButton.useAutoClickIntervalCurve));

                autoClickInterval = serializedObject.FindProperty(nameof(autoButton.autoClickInterval));

                autoClickIntervalCurve = serializedObject.FindProperty(nameof(autoButton.autoClickIntervalCurve));
            }

            public override void OnInspectorGUI()
            {
                serializedObject.ScriptField();

                EditorGUILayout.Space();

                base.OnInspectorGUI();

                serializedObject.Update();

                EditorGUILayout.PropertyField(autoClickThreshold);

                EditorGUILayout.PropertyField(useAutoClickIntervalCurve);

                EditorGUILayout.PropertyField(autoClickInterval);

                EditorGUILayout.PropertyField(autoClickIntervalCurve);

                serializedObject.ApplyModifiedProperties();
            }
        }

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

        protected override void OnDisable()
        {
            StopAutoClick();
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (IsClicked(eventData) == false)
            {
                return;
            }

            pressedTime = 0f;

            StartAutoClick();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            StopAutoClick();
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (IsClicked(eventData) == false)
            {
                return;
            }

            if (pressedTime >= autoClickThreshold)
            {
                return;
            }

            Click();
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
                yield return WaitForSecondsCache.Get(interval);

                pressedTime += interval;

                Click();

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

        private bool IsClicked(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
            {
                return false;
            }

            if (IsActive() == false || IsInteractable() == false)
            {
                return false;
            }

            return true;
        }

        private void Click()
        {
            UISystemProfilerApi.AddMarker("Button.onClick", this);

            onClick.Invoke();
        }
    }
}