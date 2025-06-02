using DG.Tweening;

using DG.Tweening.Core;

using DG.Tweening.Plugins.Options;

using System;

using UnityEngine;

namespace ZL.Unity.Tweening
{
    [Serializable]

    public sealed class QuaternionTweener : ValueTweener<Quaternion, Vector3, QuaternionOptions>
    {
        [Space]

        [SerializeField]

        private RotateMode rotateMode = RotateMode.Fast;

        public void SetRotateMode(int rotateMode)
        {
            SetRotateMode((RotateMode)rotateMode);
        }

        public void SetRotateMode(RotateMode rotateMode)
        {
            this.rotateMode = rotateMode;
        }

        public override void Play()
        {
            if (rotateMode == RotateMode.Fast)
            {
                Current.SetRotateMode(rotateMode);
            }

            base.Play();
        }

        protected override TweenerCore<Quaternion, Vector3, QuaternionOptions> To(DOGetter<Quaternion> getter, DOSetter<Quaternion> setter, in Vector3 endValue, float duration)
        {
            return DOTween.To(getter, setter, endValue, duration);
        }
    }
}