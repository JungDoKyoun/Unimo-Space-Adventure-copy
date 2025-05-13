using DG.Tweening.Plugins.Options;

using UnityEngine;

namespace ZL.Unity.Tweening
{
    [AddComponentMenu("ZL/Tweening/Canvas Group Alpha Tweener")]

    [RequireComponent(typeof(CanvasGroup))]

    public class CanvasGroupAlphaTweener : ObjectValueTweener<FloatTweener, float, float, FloatOptions>
    {
        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [EmptyField]

        protected CanvasGroup canvasGroup;

        public override float Value
        {
            get => canvasGroup.alpha;
            
            set => canvasGroup.alpha = value;
        }
    }
}