using DG.Tweening.Plugins.Options;

using UnityEngine;

namespace ZL.Unity.Tweening
{
    [AddComponentMenu("ZL/Tweening/Rotation Key Frame Tweener")]

    public sealed class RotationKeyFrameTweener : KeyFrameTweener<RotationTweener, QuaternionTweener, Quaternion, Vector3, QuaternionOptions>
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