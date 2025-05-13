using DG.Tweening.Plugins.Options;

using UnityEngine;

namespace ZL.Unity.Tweening
{
    [AddComponentMenu("ZL/Tweening/Local Rotation Tweener")]

    public sealed class LocalRotationTweener : ObjectValueTweener<QuaternionTweener, Quaternion, Vector3, QuaternionOptions>
    {
        public override Quaternion Value
        {
            get => transform.localRotation;

            set => transform.localRotation = value;
        }
    }
}