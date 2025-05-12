using UnityEngine;

namespace ZL.Unity.Tweening
{
    [AddComponentMenu("ZL/Tweening/Canvas Group Fader")]

    [RequireComponent(typeof(CanvasGroupAlphaTweener))]

    public class CanvasGroupFader : AlphaFader<CanvasGroupAlphaTweener>
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponentInParentOnly]

        [ReadOnly(true)]

        private CanvasGroupFaderGroup screenFaderGroup = null;

        public override void FadeIn(float duration = -1f)
        {
            screenFaderGroup?.SwapCurrent(this);

            base.FadeIn(duration);
        }
    }
}