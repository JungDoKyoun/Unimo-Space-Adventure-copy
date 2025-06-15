using UnityEngine;

using UnityEngine.Animations;

using ZL.Unity.Phys;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Monster 4 (Pooled)")]

    public sealed class Monster4 : Enemy, IDamager
    {
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

            if (isDashing == false)
            {
                return;
            }

            if (enemyData.MoveSpeed != 0f)
            {
                float movementSpeed = enemyData.MoveSpeed * Time.fixedDeltaTime;

                var nextPosition = rigidbody.position + rigidbody.rotation * Vector3.forward * movementSpeed;

                rigidbody.MovePosition(nextPosition);
            }
        }

        public void GiveDamage(IDamageable damageable, Vector3 contact)
        {
            damageable.TakeDamage(enemyData.AttackPower, contact);
        }

        protected override void OnDisappear()
        {
            base.OnDisappear();

            OnDisappeared();
        }

        public void Dash()
        {
            isDashing = true;
        }
    }
}