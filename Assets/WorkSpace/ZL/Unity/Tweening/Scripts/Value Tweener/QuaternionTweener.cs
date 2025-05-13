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
        protected override TweenerCore<Quaternion, Vector3, QuaternionOptions> To(DOGetter<Quaternion> getter, DOSetter<Quaternion> setter, in Vector3 endValue, float duration)
        {
            return DOTween.To(getter, setter, endValue, duration);
        }
    }
}