using UnityEngine;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Monster 4 (Spawned)")]

    public sealed class Monster4 : Enemy, IDamager
    {
        [Space]

        [SerializeField]

        private float chargeDashTime = 0f;

        public override void Appear()
        {
            movementSpeed = 0f;

            animatorGroup.SetFloat(nameof(chargeDashTime), chargeDashTime);

            base.Appear();
        }

        public override void Disappear()
        {
            base.Disappear();

            OnDisappeared();
        }

        public void GiveDamage(IDamageable damageable, Vector3 contact)
        {
            damageable.TakeDamage(enemyData.AttackPower, contact);
        }

        public void Dash()
        {
            movementSpeed = enemyData.MovementSpeed;
        }
    }
}