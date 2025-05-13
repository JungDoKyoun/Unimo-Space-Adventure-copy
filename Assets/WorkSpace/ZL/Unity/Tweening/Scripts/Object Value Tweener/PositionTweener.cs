using DG.Tweening.Plugins.Options;

using UnityEngine;

namespace ZL.Unity.Tweening
{
    [AddComponentMenu("ZL/Tweening/Position Tweener")]

    public sealed class PositionTweener : ObjectValueTweener<Vector3Tweener, Vector3, Vector3, VectorOptions>
    {
        public override Vector3 Value
        {
            get => transform.position;
            
            set => transform.position = value;
        }
    }
}