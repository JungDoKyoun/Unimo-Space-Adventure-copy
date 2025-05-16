// Working

#pragma warning disable

using UnityEngine;

using UnityEngine.InputSystem;

namespace ZL.Unity.Phys
{
    [AddComponentMenu("ZL/Phys/Rigidbody Player Input")]

    public class RigidbodyPlayerInput : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        private RigidbodyCharacterController rigidbodyCharacterController = null;

        [Space]

        [SerializeField]

        private Transform virtualCameraFollower = null;

        [Space]

        [SerializeField]

        private float jumpPower = 10f;

        public float JumpPower
        {
            get => jumpPower;

            set => jumpPower = value;
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

        }
    }
}