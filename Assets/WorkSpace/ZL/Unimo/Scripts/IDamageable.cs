using UnityEngine;

namespace ZL.Unity.Unimo
{
    public interface IDamageable
    {
        public LayerMask DamagerLayerMask { get; }

        public float CurrentHealth { get; }

        public void TakeDamage(int damage);
    }
}