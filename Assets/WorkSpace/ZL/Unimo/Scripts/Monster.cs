using UnityEngine;

namespace ZL.Unity.Unimo
{
    public abstract class Monster : MonoBehaviour, IDamageable
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponentInChildren]

        [ReadOnly(true)]

        private Animator animator = null;

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [ReadOnly(true)]

        //#pragma warning disable CS0109

        protected new Rigidbody rigidbody = null;

        //#pragma warning restore CS0109

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        protected MonsterData monsterData = null;

        [SerializeField]

        protected float lookSpeed = 0f;

        [Space]

        [SerializeField]

        private LayerMask damagerLayerMask = 0;

        public LayerMask DamagerLayerMask
        {
            get => damagerLayerMask;
        }

        protected float currentHealth = 0;

        public float CurrentHealth
        {
            get => currentHealth;
        }

        protected virtual void OnEnable()
        {
            if (MonsterManager.Instance.Target != null)
            {
                transform.LookAt(MonsterManager.Instance.Target);
            }

            currentHealth = monsterData.MaxHealth;
        }

        private void OnDisable()
        {
            if (animator != null)
            {
                animator.Rebind();
            }
        }

        public void OnCollisionStay(Collision collision)
        {
            if (damagerLayerMask.Contains(collision.gameObject.layer) == false)
            {
                return;
            }

            if (collision.gameObject.TryGetComponent<IDamager>(out var damager) == true)
            {
                damager.GiveDamage(this);
            }
        }

        public virtual void TakeDamage(int damage)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                currentHealth = 0;

                Dead();
            }
        }

        public virtual void Dead()
        {
            gameObject.SetActive(false);
        }
    }
}