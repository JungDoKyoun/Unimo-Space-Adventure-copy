using DG.Tweening;

using DG.Tweening.Core;

using DG.Tweening.Plugins.Options;

using System;

using UnityEngine;

namespace ZL.Unity.Tweening
{
    [Serializable]

    public sealed class Vector3Tweener : ValueTweener<Vector3, Vector3, VectorOptions>
    {
        protected override TweenerCore<Vector3, Vector3, VectorOptions> To(DOGetter<Vector3> getter, DOSetter<Vector3> setter, in Vector3 endValue, float duration)
        {
            return DOTween.To(getter, setter, endValue, duration);
        }
    }
}