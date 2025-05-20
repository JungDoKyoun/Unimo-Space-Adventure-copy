using UnityEngine;

using UnityEngine.Animations;

using ZL.Unity.Phys;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Monster 1")]

    public sealed class Monster1 : Monster, IDamager
    {
        private void FixedUpdate()
        {
            if (Target == null)
            {
                return;
            }

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

        public void GiveDamage(IDamageable damageable, Vector3 contact)
        {
            damageable.TakeDamage(monsterData.AttackPower, contact);
        }
    }
}