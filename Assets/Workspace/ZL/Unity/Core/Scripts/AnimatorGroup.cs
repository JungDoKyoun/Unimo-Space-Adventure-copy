using System.Collections.Generic;

using UnityEngine;

namespace ZL.Unity
{
    [AddComponentMenu("ZL/Animator Group")]

    public sealed class AnimatorGroup : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [ReadOnlyWhenPlayMode]

        [Button("FindAnimators")]

        [Margin]

        private Animator mainAnimator = null;

        [Space]

        [SerializeField]

        private List<Animator> childAnimators = null;

        private int childAnimatorsCount = 0;

        #if UNITY_EDITOR

        public void FindAnimators()
        {
            if (transform.TryGetComponentInChildren(out mainAnimator) == false)
            {
                return;
            }

            mainAnimator.TryGetComponentsInChildrenOnly(out childAnimators);

            FixedEditorUtility.SetDirty(this);
        }

        #endif

        private void Awake()
        {
            childAnimatorsCount = childAnimators.Count;
        }

        public void SetInteger(AnimationEvent animationEvent)
        {
            SetInteger(animationEvent.stringParameter, animationEvent.intParameter);
        }

        public void SetInteger(string name, int value)
        {
            mainAnimator.SetInteger(name, value);

            for (int i = 0; i < childAnimatorsCount; ++i)
            {
                childAnimators[i].SetInteger(name, value);
            }
        }

        public void SetFloat(AnimationEvent animationEvent)
        {
            SetFloat(animationEvent.stringParameter, animationEvent.floatParameter);
        }

        public void SetFloat(string name, float value)
        {
            mainAnimator.SetFloat(name, value);

            for (int i = 0; i < childAnimatorsCount; ++i)
            {
                childAnimators[i].SetFloat(name, value);
            }
        }

        public void SetBoolTrue(string name)
        {
            SetBool(name, true);
        }

        public void SetBoolFalse(string name)
        {
            SetBool(name, false);
        }

        public void SetBool(string name, bool value)
        {
            mainAnimator.SetBool(name, value);

            for (int i = 0; i < childAnimatorsCount; ++i)
            {
                childAnimators[i].SetBool(name, value);
            }
        }

        public void SetTrigger(string name)
        {
            mainAnimator.SetTrigger(name);

            for (int i = 0; i < childAnimatorsCount; ++i)
            {
                childAnimators[i].SetTrigger(name);
            }
        }

        public void Rebind()
        {
            mainAnimator.Rebind();

            for (int i = 0; i < childAnimatorsCount; ++i)
            {
                childAnimators[i].Rebind();
            }
        }
    }
}