using UnityEngine;

using UnityEngine.Animations;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Monster 1 (Spawned)")]

    public sealed class Monster1 : Enemy, IDamager
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

            if (spawner == null)
            {
                return;
            }

            CheckDistanceToSpawner();
        }

        public void GiveDamage(IDamageable damageable, Vector3 contact)
        {
            damageable.TakeDamage(enemyData.AttackPower, contact);
        }
    }
}