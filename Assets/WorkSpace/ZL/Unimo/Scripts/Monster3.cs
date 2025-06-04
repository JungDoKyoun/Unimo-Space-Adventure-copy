using UnityEngine;

using UnityEngine.Animations;

using ZL.Unity.Phys;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Monster 3 (Pooled)")]

    public sealed class Monster3 : Enemy, IDamager
    {
        [Space]

        [SerializeField]

        private float dashSpeedMultiply = 0f;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private ArcedDetector detector = null;

        private bool isDashing = false;

        protected override void OnEnable()
        {
            base.OnEnable();

            isDashing = false;
        }

        private void FixedUpdate()
        {
            if (isStoped == true)
            {
                return;
            }

            if (rotationSpeed != 0f)
            {
                rigidbody.LookTowards(Destination, Axis.Y, rotationSpeed);
            }

            if (enemyData.MoveSpeed != 0f)
            {
                float movementSpeed = enemyData.MoveSpeed * Time.fixedDeltaTime;

                if (isDashing == true)
                {
                    movementSpeed *= dashSpeedMultiply;
                }

                var nextPosition = rigidbody.position + rigidbody.rotation * Vector3.forward * movementSpeed;

                rigidbody.MovePosition(nextPosition);
            }
        }

        private void Update()
        {
            if (detector.Detect(Destination) == false)
            {
                return;
            }

            isStoped = true;

            detector.enabled = false;

            animator.SetTrigger("Encounter");
        }

        public override void OnAppeared()
        {
            base.OnAppeared();

            detector.enabled = true;
        }

        protected override void OnDisappear()
        {
            base.OnDisappear();

            detector.enabled = false;
        }

        public void Dash()
        {
            isStoped = false;

            isDashing = true;
        }

        public void GiveDamage(IDamageable damageable, Vector3 contact)
        {
            damageable.TakeDamage(enemyData.AttackPower, contact);
        }
    }
}