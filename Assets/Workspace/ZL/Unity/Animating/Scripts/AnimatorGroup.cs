using System.Collections.Generic;

using UnityEngine;

namespace ZL.Unity.Animating
{
    [AddComponentMenu("ZL/Animating/Animator Group")]

    public sealed class AnimatorGroup : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [ReadOnlyWhenPlayMode]

        [Button("Crawling")]

        [Margin]

        private Animator mainAnimator = null;

        [Space]

        [SerializeField]

        private List<Animator> childAnimators = null;

        private int childAnimatorsCount = 0;

        private void Awake()
        {
            childAnimatorsCount = childAnimators.Count;
        }

        #if UNITY_EDITOR

        public void Crawling()
        {
            if (transform.TryGetComponentInChildren(out mainAnimator) == false)
            {
                return;
            }

            if (mainAnimator.transform.TryGetComponentsInChildrenOnly<Animator>(out childAnimators) == false)
            {
                return;
            }

            FixedEditorUtility.SetDirty(this);
        }

        #endif

        public void SetInteger(string name, int value)
        {
            mainAnimator.SetInteger(name, value);

            for (int i = 0; i < childAnimatorsCount; ++i)
            {
                childAnimators[i].SetInteger(name, value);
            }
        }

        public void SetFloat(string name, float value)
        {
            mainAnimator.SetFloat(name, value);

            for (int i = 0; i < childAnimatorsCount; ++i)
            {
                childAnimators[i].SetFloat(name, value);
            }
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