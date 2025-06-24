using UnityEngine;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Monster 1 (Spawned)")]

    public sealed class Monster1 : Enemy, IDamager
    {
        public void GiveDamage(IDamageable damageable, Vector3 contact)
        {
            damageable.TakeDamage(enemyData.AttackPower, contact);
        }
    }
}