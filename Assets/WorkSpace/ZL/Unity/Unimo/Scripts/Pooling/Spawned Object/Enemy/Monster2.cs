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

        [SerializeField]

        private string projectileName = "";

        [SerializeField]

        private float attackCooldown = 0f;

        [SerializeField]

        private float attackDistance = 0f;

        private float attackCooldownTimer = 0f;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private Transform muzzle = null;

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

            if (transform.position.DistanceTo(Destination.position, Axis.Y) <= stopDistance)
            {
                animator.SetBool("IsMoving", false);
            }

            else if (enemyData.MovementSpeed != 0f)
            {
                animator.SetBool("IsMoving", true);

                rigidbody.MoveForward(enemyData.MovementSpeed * Time.fixedDeltaTime);
            }

            CheckDistanceToSpawner();
        }

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
                if (transform.position.DistanceTo(Destination.position, Axis.Y) > attackDistance)
                {
                    return;
                }
            }

            attackCooldownTimer = attackCooldown;

            animator.SetTrigger("Attack");
        }

        public void Shoot()
        {
            var projectile = ObjectPoolManager.Instance.Clone(projectileName);

            projectile.transform.SetPositionAndRotation(muzzle);

            projectile.Appear();
        }

        public void GiveDamage(IDamageable damageable, Vector3 contact)
        {
            damageable.TakeDamage(enemyData.AttackPower, contact);
        }

        public override void OnDisappeared()
        {
            base.OnDisappeared();

            attackCooldownTimer = 0f;
        }
    }
}