using DG.Tweening.Plugins.Options;

using UnityEngine;

namespace ZL.Unity.Tweening
{
    [AddComponentMenu("ZL/Tweening/Rotation Key Frame Tweener")]

    [RequireComponent(typeof(RotationTweener))]

    public sealed class RotationKeyFrameTweener : KeyFrameTweener<QuaternionTweener, Quaternion, Vector3, QuaternionOptions, RotationTweener>
    {
        public override void SetKeyFrame(int index)
        {
            keyFrames.Index = index;

            if (keyFrames.TryGetCurrent(out var eulerAngles) == true)
            {
                transform.rotation = Quaternion.Euler(eulerAngles);
            }
        }
    }
}