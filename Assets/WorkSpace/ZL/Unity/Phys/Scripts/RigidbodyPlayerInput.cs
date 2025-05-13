using UnityEngine;

using UnityEngine.InputSystem;

namespace ZL.Unity.Phys
{
    [AddComponentMenu("ZL/Phys/Rigidbody Player Input")]

    [DisallowMultipleComponent]

    [RequireComponent(typeof(PlayerInput))]

    [RequireComponent(typeof(RigidbodyCharacterController))]

    public class RigidbodyPlayerInput : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [ReadOnly(true)]

        private RigidbodyCharacterController rigidbodyCharacterController;

        [Space]

        [SerializeField]

        private Transform virtualCameraFollower;

        [Space]

        [SerializeField]

        private float jumpPower = 10f;

        public float JumpPower
        {
            get => jumpPower;

            set => jumpPower = value;
        }

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        protected Vector2 lookDelta;

        private void FixedUpdate()
        {
            //virtualCameraFollower.Rotate
        }

        protected virtual void OnMove(InputValue inputValue)
        {
            rigidbodyCharacterController.MovementDirection = inputValue.Get<Vector3>();
        }

        protected virtual void OnJump(InputValue inputValue)
        {
            if (rigidbodyCharacterController.IsGrounded == true)
            {
                rigidbodyCharacterController.ImpulseForce = jumpPower * inputValue.Get<Vector3>();
            }
        }

        protected virtual void OnLook(InputValue inputValue)
        {
            lookDelta = inputValue.Get<Vector2>();
        }
    }
}