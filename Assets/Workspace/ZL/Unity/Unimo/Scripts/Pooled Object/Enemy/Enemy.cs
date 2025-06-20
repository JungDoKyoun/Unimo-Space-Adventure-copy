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

        [ReadOnlyWhenPlayMode]

        protected Animator animator = null;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        protected EnemyData enemyData = null;

        protected float rotationSpeed = -1f;

        public float RotationSpeed
        {
            set => rotationSpeed = value;
        }

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

        protected override void OnDisable()
        {
            rigidbody.velocity = Vector3.zero;

            if (animator != null)
            {
                animator.Rebind();
            }

            rotationSpeed = -1f;

            base.OnDisable();
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

        public override void Appear()
        {
            currentHealth = enemyData.MaxHealth;

            if (rotationSpeed == -1f)
            {
                rotationSpeed = enemyData.RotationSpeed;
            }

            base.Appear();
        }

        public override void OnAppeared()
        {
            collider.enabled = true;

            isStoped = false;

            base.OnAppeared();
        }

        public override void Disappear()
        {
            base.Disappear();

            collider.enabled = false;

            isStoped = true;
        }

        protected override void OnDisappear()
        {
            if (animator != null)
            {
                animator.SetTrigger("Disappear");
            }
        }
    }
}