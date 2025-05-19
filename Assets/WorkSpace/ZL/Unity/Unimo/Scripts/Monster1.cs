using UnityEngine;

using ZL.Unity.Phys;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Monster 1")]

    public sealed class Monster1 : Monster, IDamager
    {
        private void FixedUpdate()
        {
            if (MonsterManager.Instance.Target == null)
            {
                return;
            }

            if (monsterData.MoveSpeed != 0f)
            {
                //rigidbody.MoveTowards(Player.Instance.transform.position, monsterData.MoveSpeed);

                //rigidbody.MoveTowards(rigidbody.position + transform.forward.normalized, monsterData.MoveSpeed);

                var forwardMove = rigidbody.rotation * Vector3.forward * monsterData.MoveSpeed * Time.fixedDeltaTime;

                rigidbody.MovePosition(rigidbody.position + forwardMove);
            }

            if (lookSpeed != 0f)
            {
                rigidbody.LookTowards(MonsterManager.Instance.Target.position, lookSpeed);
            }
        }

        public void GiveDamage(IDamageable damageable)
        {
            damageable.TakeDamage(monsterData.AttackPower);
        }
    }
}