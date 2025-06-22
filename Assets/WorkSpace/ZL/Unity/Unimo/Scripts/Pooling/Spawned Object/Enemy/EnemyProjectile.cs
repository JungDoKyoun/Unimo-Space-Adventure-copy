using UnityEngine;

using UnityEngine.Animations;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Enemy Projectile (Spawned)")]

    public sealed class EnemyProjectile : Enemy, IDamager
    {
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

            if (enemyData.MovementSpeed != 0f)
            {
                rigidbody.MoveForward(enemyData.MovementSpeed * Time.fixedDeltaTime);
            }

            CheckDistanceToSpawner();
        }

        public override void Appear()
        {
            base.Appear();

            OnAppeared();
        }

        protected override void OnDisappear()
        {
            OnDisappeared();
        }

        public void GiveDamage(IDamageable damageable, Vector3 contact)
        {
            damageable.TakeDamage(enemyData.AttackPower, contact);
        }
    }
}