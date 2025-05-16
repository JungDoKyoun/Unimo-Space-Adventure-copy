using UnityEngine;

namespace ZL.Unity
{
    [AddComponentMenu("ZL/Spinner")]

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