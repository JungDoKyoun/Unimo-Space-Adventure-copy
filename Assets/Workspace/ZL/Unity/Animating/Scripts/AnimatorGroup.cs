using UnityEngine;

namespace ZL.Unity.Animating
{
    [AddComponentMenu("ZL/Animating/Animator Group")]

    public sealed class AnimatorGroup : Animator
    {
        [Space]

        [SerializeField]

        private Animator[] animators = null;

        public void SetFloatAll(AnimationEvent animationEvent)
        {
            SetFloatAll(animationEvent.stringParameter, animationEvent.floatParameter);
        }

        public void SetFloatAll(string name, float value)
        {
            SetFloat(name, value);

            for (int i = 0; i < animators.Length; ++i)
            {
                animators[i].SetFloat(name, value);
            }
        }

        public void SetBoolAll(string name, bool value)
        {
            SetBoolAll(name, value);

            for (int i = 0; i < animators.Length; ++i)
            {
                animators[i].SetBool(name, value);
            }
        }

        public void SetIntegerAll(AnimationEvent animationEvent)
        {
            SetIntegerAll(animationEvent.stringParameter, animationEvent.intParameter);
        }

        public void SetIntegerAll(string name, int value)
        {
            SetInteger(name, value);

            for (int i = 0; i < animators.Length; ++i)
            {
                animators[i].SetInteger(name, value);
            }
        }

        public void SetTriggerAll(string name)
        {
            SetTrigger(name);

            for (int i = 0; i < animators.Length; ++i)
            {
                animators[i].SetTrigger(name);
            }
        }

        public void RebindAll()
        {
            Rebind();

            for (int i = 0; i < animators.Length; ++i)
            {
                animators[i].Rebind();
            }
        }
    }
}