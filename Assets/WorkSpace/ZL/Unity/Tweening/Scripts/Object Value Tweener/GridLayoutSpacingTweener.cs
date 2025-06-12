using DG.Tweening.Plugins.Options;

using UnityEngine;

using UnityEngine.UI;

namespace ZL.Unity.Tweening
{
    [AddComponentMenu("ZL/Tweening/Grid Layout Spacing Tweener")]

    public sealed class GridLayoutSpacingTweener : ObjectValueTweener<Vector2Tweener, Vector2, Vector2, VectorOptions>
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        private GridLayoutGroup gridLayoutGroup = null;

        public override Vector2 Value
        {
            get => gridLayoutGroup.spacing;

            set => gridLayoutGroup.spacing = value;
        }
    }
}