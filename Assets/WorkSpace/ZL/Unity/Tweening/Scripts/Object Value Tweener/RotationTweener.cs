using DG.Tweening;

using DG.Tweening.Plugins.Options;

using UnityEngine;

namespace ZL.Unity.Tweening
{
    [AddComponentMenu("ZL/Tweening/Rotation Tweener")]

    public sealed class RotationTweener : ObjectValueTweener<QuaternionTweener, Quaternion, Vector3, QuaternionOptions>
    {
        public RotateMode RotateMode
        {
            get => valueTweener.RotateMode;
            
            set => valueTweener.RotateMode = value;
        }

        public override Quaternion Value
        {
            get => transform.rotation;

            set => transform.rotation = value;
        }
    }
}