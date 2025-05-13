using DG.Tweening.Plugins.Options;

using UnityEngine;

using UnityEngine.UI;

namespace ZL.Unity.Tweening
{
    [AddComponentMenu("ZL/Tweening/Graphic Color Tweener")]

    public sealed class GraphicColorTweener : ObjectValueTweener<ColorTweener, Color, Color, ColorOptions>
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [ReadOnly(true)]

        private Graphic graphic;

        public override Color Value
        {
            get => graphic.color;

            set => graphic.color = value;
        }
    }
}