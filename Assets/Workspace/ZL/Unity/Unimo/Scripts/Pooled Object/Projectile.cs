using UnityEngine;

using ZL.Unity.Pooling;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Projectile (Pooled)")]

    public sealed class Projectile : PooledObject, IDamager
    {
        [Space]

        [SerializeField]

        private float speed = 0f;

        [SerializeField]

        private float damage = 0f;

        private void FixedUpdate()
        {
            transform.position += speed * Time.fixedDeltaTime * transform.forward;
        }

        public void GiveDamage(IDamageable damageable, Vector3 contact)
        {
            damageable.TakeDamage(damage, contact);

            Disappear();
        }
    }
}