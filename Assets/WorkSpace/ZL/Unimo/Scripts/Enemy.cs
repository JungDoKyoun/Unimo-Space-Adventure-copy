using UnityEngine;

using ZL.Unity.Pooling;

namespace ZL.Unity.Unimo
{
    public abstract class Enemy : PooledObject, IDamageable
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        #pragma warning disable CS0108

        private Collider collider = null;

        #pragma warning restore CS0108

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        #pragma warning disable CS0108

        protected Rigidbody rigidbody = null;

        #pragma warning restore CS0108

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponentInChildren]

        [ReadOnly(true)]

        protected Animator animator = null;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        protected EnemyData enemyData = null;

        [SerializeField]

        private float lifeTime = -1f;

        [SerializeField]

        protected float rotationSpeed = 0f;

        protected float currentHealth = 0f;

        public float CurrentHealth
        {
            get => currentHealth;
        }

        private EnemyManager enemyManager = null;

        protected Transform Destination
        {
            get => enemyManager.Destination;
        }

        protected bool isStoped = true;

        private void Awake()
        {
            enemyManager = EnemyManager.Instance;
        }

        protected virtual void OnEnable()
        {
            currentHealth = enemyData.MaxHealth;

            if (lifeTime > 0f)
            {
                Invoke(nameof(Disappear), lifeTime);
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            rigidbody.velocity = Vector3.zero;

            if (animator != null)
            {
                animator.Rebind();
            }

            #if UNITY_EDITOR

            OnDisappear();

            #endif
        }

        public virtual void TakeDamage(float damage, Vector3 contact)
        {
            currentHealth -= damage;

            if (currentHealth <= 0f)
            {
                currentHealth = 0f;

                Disappear();
            }
        }

        public virtual void OnAppeared()
        {
            collider.enabled = true;

            isStoped = false;
        }

        private void Disappear()
        {
            CancelInvoke(nameof(Disappear));

            OnDisappear();

            animator.SetTrigger("Disappear");
        }

        protected virtual void OnDisappear()
        {
            collider.enabled = false;

            isStoped = true;
        }

        public void OnDisappeared()
        {
            gameObject.SetActive(false);
        }
    }
}