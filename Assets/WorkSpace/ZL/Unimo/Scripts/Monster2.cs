using UnityEngine;

using UnityEngine.Animations;

using ZL.Unity.Phys;

using ZL.Unity.Pooling;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Monster 2")]

    public sealed class Monster2 : Monster, IDamager
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private Transform muzzle = null;

        [SerializeField]

        private float attackInterval = 0f;

        private float attackIntervalTimer = 0f;

        private void FixedUpdate()
        {
            if (Target == null)
            {
                animator.SetBool("IsMoving", false);

                return;
            }

            Movement();

            Attack();
        }

        private void Movement()
        {
            animator.SetBool("IsMoving", true);

            if (monsterData.MoveSpeed != 0f)
            {
                //rigidbody.MoveTowards(MonsterManager.Instance.Target, monsterData.MoveSpeed);

                var forwardMove = rigidbody.rotation * Vector3.forward * monsterData.MoveSpeed * Time.fixedDeltaTime;

                rigidbody.MovePosition(rigidbody.position + forwardMove);
            }

            if (rotationSpeed != 0f)
            {
                rigidbody.LookTowards(Target, Axis.Y, rotationSpeed);
            }
        }

        private void Attack()
        {
            if (attackIntervalTimer > 0f)
            {
                attackIntervalTimer -= Time.fixedDeltaTime;

                return;
            }

            attackIntervalTimer = attackInterval;

            animator.SetTrigger("Attack");
        }

        public void ShootProjectile()
        {
            var bullet = ObjectPoolManager.Instance.Cloning("Enemy Projectile 1");

            bullet.transform.SetPositionAndRotation(muzzle);

            bullet.SetActive(true);
        }

        public void GiveDamage(IDamageable damageable, Vector3 contact)
        {
            //damageable.TakeDamage(monsterData.AttackPower, contact);
        }
    }
}