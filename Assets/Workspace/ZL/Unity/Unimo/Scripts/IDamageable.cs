using UnityEngine;

namespace ZL.Unity.Unimo
{
    public interface IDamageable
    {
        public float CurrentHealth { get; }

        public void TakeDamage(float damage, Vector3 contact);
    }
}