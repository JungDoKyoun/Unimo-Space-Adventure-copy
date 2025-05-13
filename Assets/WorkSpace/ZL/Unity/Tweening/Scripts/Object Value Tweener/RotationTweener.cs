using DG.Tweening;

using DG.Tweening.Plugins.Options;

using UnityEngine;

namespace ZL.Unity.Tweening
{
    [AddComponentMenu("ZL/Tweening/Rotation Tweener")]

    public sealed class RotationTweener : ObjectValueTweener<QuaternionTweener, Quaternion, Vector3, QuaternionOptions>
    {
        [Space]

        [SerializeField]

        private RotateMode rotateMode;

        public override Quaternion Value
        {
            get => transform.rotation;

            set => transform.rotation = value;
        }
    }
}