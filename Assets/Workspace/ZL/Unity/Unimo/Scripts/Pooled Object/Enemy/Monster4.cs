using UnityEngine;

using UnityEngine.Animations;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Monster 4 (Pooled)")]

    public sealed class Monster4 : Enemy, IDamager
    {
        private bool isDashing = false;

        private void FixedUpdate()
        {
            if (isStoped == true)
            {
                return;
            }
            
            if (rotationSpeed != 0f)
            {
                rigidbody.LookTowards(Destination.position, rotationSpeed * Time.fixedDeltaTime, Axis.Y);
            }

            if (isDashing == false)
            {
                return;
            }

            if (enemyData.MovementSpeed != 0f)
            {
                rigidbody.MoveForward(enemyData.MovementSpeed * Time.fixedDeltaTime);
            }
        }

        public override void Disappear()
        {
            base.Disappear();

            isDashing = false;

            OnDisappeared();
        }

        public void GiveDamage(IDamageable damageable, Vector3 contact)
        {
            damageable.TakeDamage(enemyData.AttackPower, contact);
        }

        public void Dash()
        {
            isDashing = true;
        }
    }
}