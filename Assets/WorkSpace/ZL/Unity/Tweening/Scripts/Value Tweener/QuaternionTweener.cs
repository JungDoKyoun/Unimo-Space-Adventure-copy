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

        public RotateMode RotateMode
        {
            get => rotateMode;

            set => rotateMode = value;
        }

        protected override TweenerCore<Quaternion, Vector3, QuaternionOptions> To(DOGetter<Quaternion> getter, DOSetter<Quaternion> setter, in Vector3 endValue, float duration)
        {
            return DOTween.To(getter, setter, endValue, duration);
        }

        public override TweenerCore<Quaternion, Vector3, QuaternionOptions> Tween(Vector3 endValue, float duration = -1)
        {
            base.Tween(endValue, duration);

            Current.SetRotateMode(rotateMode);

            return Current;
        }
    }
}