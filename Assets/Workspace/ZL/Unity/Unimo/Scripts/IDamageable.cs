using UnityEngine;

namespace ZL.Unity.Unimo
{
    public interface IDamageable
    {
        public Collider MainCollider { get; }

        public float CurrentHealth { get; }

        public void TakeDamage(float damage, Vector3 contact);
    }
}