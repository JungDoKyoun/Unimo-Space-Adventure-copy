using UnityEngine;

using UnityEngine.Animations;

using ZL.Unity.Pooling;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Monster 2 (Spawned)")]

    public sealed class Monster2 : Enemy, IDamager
    {
        [Space]

        [SerializeField]

        private float stopDistance = 0f;

        [Space]

        [SerializeField]

        private float attackCooldownTime = 0f;

        [SerializeField]

        private float attackRange = 0f;

        private float attackCooldownTimer = 0f;

        [Space]

        [SerializeField]

        private string projectileName = "";

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private Transform muzzle = null;

        private void Update()
        {
            if (isStoped == true)
            {
                return;
            }

            if (attackCooldownTimer > 0f)
            {
                attackCooldownTimer -= Time.deltaTime;

                return;
            }

            if (Destination != null)
            {
                if (transform.position.DistanceTo(Destination.position, Axis.Y) > attackRange)
                {
                    return;
                }
            }

            attackCooldownTimer = attackCooldownTime;

            animatorGroup.SetTrigger("Attack");
        }

        public override void OnDisappeared()
        {
            base.OnDisappeared();

            attackCooldownTimer = 0f;
        }

        protected override void Move()
        {
            if (transform.position.DistanceTo(Destination.position, Axis.Y) > stopDistance)
            {
                movementSpeed = enemyData.MovementSpeed;
            }

            else
            {
                movementSpeed = 0f;
            }

            base.Move();
        }

        public void GiveDamage(IDamageable damageable, Vector3 contact)
        {
            damageable.TakeDamage(enemyData.AttackPower, contact);
        }

        public void Shoot()
        {
            var projectile = ObjectPoolManager.Instance.Clone(projectileName);

            projectile.transform.SetPositionAndRotation(muzzle);

            projectile.Appear();
        }
    }
}