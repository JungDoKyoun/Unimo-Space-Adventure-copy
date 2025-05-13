using UnityEngine;

namespace ZL.Unity.Motions
{
    public abstract class TransformMotion : MonoBehaviour
    {
        [Space]

        public Vector3 direction = Vector3.zero;

        public float speed = 1f;

        protected abstract void Update();
    }
}