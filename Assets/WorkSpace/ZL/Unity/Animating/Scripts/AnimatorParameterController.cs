using UnityEngine;

namespace ZL.Unity.Animating
{
    [AddComponentMenu("ZL/Animating/Animator Parameter Controller")]

    public sealed class AnimatorParameterController : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        private Animator animator = null;

        public void SetInteger(AnimationEvent animationEvent)
        {
            animator.SetInteger(animationEvent.stringParameter, animationEvent.intParameter);
        }

        public void SetFloat(AnimationEvent animationEvent)
        {
            animator.SetFloat(animationEvent.stringParameter, animationEvent.floatParameter);
        }

        public void SetBoolTrue(string key)
        {
            animator.SetBool(key, true);
        }

        public void SetBoolFalse(string key)
        {
            animator.SetBool(key, false);
        }

        public void SetTrigget(string key)
        {
            animator.SetTrigger(key);
        }
    }
}