using UnityEngine;

using UnityEngine.Animations;

using ZL.Unity.Phys;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Monster 1")]

    public sealed class Monster1 : Enemy, IDamager
    {
        private void FixedUpdate()
        {
            if (Target == null)
            {
                return;
            }

            if (isStoped == true)
            {
                return;
            }

            if (rotationSpeed != 0f)
            {
                rigidbody.LookTowards(Target, Axis.Y, rotationSpeed);
            }

            if (enemyData.MoveSpeed != 0f)
            {
                //rigidbody.MoveTowards(MonsterManager.Instance.Target, monsterData.MoveSpeed);

                var forwardMove = rigidbody.rotation * Vector3.forward * enemyData.MoveSpeed * Time.fixedDeltaTime;

                rigidbody.MovePosition(rigidbody.position + forwardMove);
            }
        }

        public void GiveDamage(IDamageable damageable, Vector3 contact)
        {
            damageable.TakeDamage(enemyData.AttackPower, contact);
        }
    }
}