using UnityEngine;

namespace ZL.Unity.Unimo
{
    public interface IDamageable
    {
        public LayerMask DamagerLayerMask { get; }

        public int CurrentHealth { get; }

        public void TakeDamage(int damage);
    }

    public interface IDamager
    {
        public void GiveDamage(IDamageable damageable);
    }
}