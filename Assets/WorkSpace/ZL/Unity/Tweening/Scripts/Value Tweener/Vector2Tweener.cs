using DG.Tweening;

using DG.Tweening.Core;

using DG.Tweening.Plugins.Options;

using System;

using UnityEngine;

namespace ZL.Unity.Tweening
{
    [Serializable]

    public sealed class Vector2Tweener : ValueTweener<Vector2, Vector2, VectorOptions>
    {
        protected override TweenerCore<Vector2, Vector2, VectorOptions> To(DOGetter<Vector2> getter, DOSetter<Vector2> setter, in Vector2 endValue, float duration)
        {
            return DOTween.To(getter, setter, endValue, duration);
        }
    }
}