using DG.Tweening;

using DG.Tweening.Core;

using DG.Tweening.Plugins.Options;

using System;

using UnityEngine;

namespace ZL.Unity.Tweening
{
    [Serializable]

    public sealed class ColorTweener : ValueTweener<Color, Color, ColorOptions>
    {
        protected override TweenerCore<Color, Color, ColorOptions> To(DOGetter<Color> getter, DOSetter<Color> setter, in Color endValue, float duration)
        {
            return DOTween.To(getter, setter, endValue, duration);
        }
    }
}