using UnityEngine;

using ZL.CS;

namespace ZL.Unity
{
    [AddComponentMenu("ZL/Ping Pong")]

    public sealed class PingPong : TransformMotion
    {
        [Space]

        [SerializeField]
        
        [Range(-2f, 2f)]

        private float theta = 0f;

        [SerializeField]
        
        [Range(0f, 1f)]

        private float sin = 0.5f;

        private float time = 0f;

        [SerializeField]

        private float delta = 0f;

        private Vector3 moveDirection = Vector3.zero;

        private void Awake()
        {
            time = theta * 0.25f / speed;
        }

        protected override void Update()
        {
            transform.position -= moveDirection;

            time += Time.deltaTime;

            theta = Mathf.Sin(MathFEx.PI2 * (time + delta) * speed) + sin;

            moveDirection = direction * theta;

            transform.position += moveDirection;
        }
    }
}