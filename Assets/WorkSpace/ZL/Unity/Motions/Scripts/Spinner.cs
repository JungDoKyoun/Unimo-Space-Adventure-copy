using UnityEngine;

namespace ZL.Unity.Motions
{
    [AddComponentMenu("ZL/Motions/Spinner")]

    public sealed class Spinner : TransformMotion
    {
        [Space]

        [SerializeField]

        private Space relativeTo = Space.Self;

        protected override void Update()
        {
            transform.Rotate(speed * Time.deltaTime * direction, relativeTo);
        }
    }
}