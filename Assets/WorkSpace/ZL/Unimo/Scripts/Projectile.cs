using UnityEngine;

using ZL.Unity.Pooling;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Projectile (Pooled)")]

    public sealed class Projectile : PooledObject, IDamager
    {
        [Space]

        [SerializeField]

        private float lifeTime = -1f;

        [SerializeField]

        private float speed = 0f;

        [SerializeField]

        private float damage = 0f;

        private void OnEnable()
        {
            if (lifeTime != -1f)
            {
                Invoke(nameof(OnDisappeared), lifeTime);
            }
        }

        private void FixedUpdate()
        {
            transform.position += speed * Time.fixedDeltaTime * transform.forward;
        }

        public void GiveDamage(IDamageable damageable, Vector3 contact)
        {
            damageable.TakeDamage(damage, contact);

            OnDisappeared();
        }

        private void OnDisappeared()
        {
            gameObject.SetActive(false);
        }
    }
}