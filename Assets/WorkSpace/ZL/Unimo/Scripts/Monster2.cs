using UnityEngine;

using UnityEngine.Animations;

using ZL.Unity.Phys;

using ZL.Unity.Pooling;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Monster 2 (Pooled)")]

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

        protected override void OnEnable()
        {
            base.OnEnable();

            attackCooldownTimer = 0f;
        }

        private void FixedUpdate()
        {
            animator.SetBool("IsMoving", false);

            if (Destination == null)
            {
                return;
            }

            if (isStoped == true)
            {
                return;
            }

            if (rotationSpeed != 0f)
            {
                rigidbody.LookTowards(Destination, Axis.Y, rotationSpeed);
            }

            if (Vector3.Distance(transform.position, Destination.position) <= stopDistance)
            {
                return;
            }

            if (enemyData.MoveSpeed != 0f)
            {
                animator.SetBool("IsMoving", true);

                var movementSpeed = enemyData.MoveSpeed * Time.fixedDeltaTime;

                var nextPosition = rigidbody.position + rigidbody.rotation * Vector3.forward * movementSpeed;

                rigidbody.MovePosition(nextPosition);
            }
        }

        private void Update()
        {
            if (attackCooldownTimer > 0f)
            {
                attackCooldownTimer -= Time.deltaTime;

                return;
            }

            if (Destination == null)
            {
                return;
            }

            if (isStoped == true)
            {
                return;
            }

            if (Vector3.Distance(transform.position, Destination.position) > attackDistance)
            {
                return;
            }

            attackCooldownTimer = attackCooldown;

            animator.SetTrigger("Attack");
        }

        public void Shoot()
        {
            var projectile = ObjectPoolManager.Instance.Cloning(projectileName);

            projectile.transform.SetPositionAndRotation(muzzle);

            projectile.gameObject.SetActive(true);
        }

        public void GiveDamage(IDamageable damageable, Vector3 contact)
        {
            damageable.TakeDamage(enemyData.AttackPower, contact);
        }
    }
}