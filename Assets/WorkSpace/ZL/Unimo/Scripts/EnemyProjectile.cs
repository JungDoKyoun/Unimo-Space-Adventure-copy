using UnityEngine;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Enemy Projectile")]

    public sealed class EnemyProjectile : MonoBehaviour, IDamager
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
            if (lifeTime > 0f)
            {
                Invoke(nameof(SetActiveFalse), lifeTime);
            }
        }

        private void OnDisable()
        {
            CancelInvoke();
        }

        private void FixedUpdate()
        {
            transform.position += speed * Time.fixedDeltaTime * transform.forward;
        }

        public void GiveDamage(IDamageable damageable, Vector3 contact)
        {
            damageable.TakeDamage(damage, contact);

            SetActiveFalse();
        }

        private void SetActiveFalse()
        {
            gameObject.SetActive(false);
        }
    }
}