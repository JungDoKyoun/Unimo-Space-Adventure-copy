using UnityEngine;

using UnityEngine.Animations;

using ZL.Unity.Phys;

using ZL.Unity.Pooling;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Monster 2")]

    public sealed class Monster2 : Enemy, IDamager
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private Transform muzzle = null;

        [SerializeField]

        private string projectileName = "";

        [SerializeField]

        private float attackInterval = 0f;

        private float attackIntervalTimer = 0f;

        private void FixedUpdate()
        {
            animator.SetBool("IsMoving", false);

            if (isStoped == true)
            {
                return;
            }

            if (enemyData.MoveSpeed != 0f)
            {
                animator.SetBool("IsMoving", true);

                //rigidbody.MoveTowards(MonsterManager.Instance.Target, monsterData.MoveSpeed);

                var forwardMove = rigidbody.rotation * Vector3.forward * enemyData.MoveSpeed * Time.fixedDeltaTime;

                rigidbody.MovePosition(rigidbody.position + forwardMove);
            }

            if (rotationSpeed != 0f)
            {
                rigidbody.LookTowards(Target, Axis.Y, rotationSpeed);
            }
        }

        private void Update()
        {
            if (attackIntervalTimer > 0f)
            {
                attackIntervalTimer -= Time.fixedDeltaTime;

                return;
            }

            if (isStoped == true)
            {
                return;
            }

            attackIntervalTimer = attackInterval;

            animator.SetTrigger("Attack");
        }

        public void OnAttack()
        {
            var bullet = ObjectPoolManager.Instance.Cloning(projectileName);

            bullet.transform.SetPositionAndRotation(muzzle);

            bullet.SetActive(true);
        }

        public void GiveDamage(IDamageable damageable, Vector3 contact)
        {
            damageable.TakeDamage(enemyData.AttackPower, contact);
        }
    }
}