using UnityEngine;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Enemy Projectile (Spawned)")]

    public sealed class EnemyProjectile : Enemy, IDamager
    {
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

            Disappear();
        }
    }
}