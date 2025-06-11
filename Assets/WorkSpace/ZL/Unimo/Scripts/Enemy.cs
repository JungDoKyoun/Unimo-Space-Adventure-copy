using UnityEngine;

using UnityEngine.Animations;

using ZL.Unity.Phys;

using ZL.Unity.Pooling;

namespace ZL.Unity.Unimo
{
    public abstract class Enemy : PooledObject, IDamageable
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [ReadOnly(true)]

        #pragma warning disable CS0108

        private Collider collider = null;

        #pragma warning restore CS0108

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [ReadOnly(true)]

        #pragma warning disable CS0108

        protected Rigidbody rigidbody = null;

        #pragma warning restore CS0108

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

        protected Transform Destination
        {
            get => EnemyManager.Instance.Destination;
        }

        protected bool isStoped = true;

        protected virtual void OnEnable()
        {
            if (Destination != null)
            {
                var forward = Destination.position - transform.position;

                rigidbody.rotation = rigidbody.LookRotation(forward, Axis.Y);
            }

            if (rigidbody != null)
            {
                rigidbody.velocity = Vector3.zero;
            }

            if (animator != null)
            {
                animator.Rebind();
            }

            if (lifeTime > 0f)
            {
                Invoke(nameof(Disappear), lifeTime);
            }

            currentHealth = enemyData.MaxHealth;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            OnDisappear();
        }

        public virtual void OnAppeared()
        {
            collider.enabled = true;

            isStoped = false;
        }

        public virtual void TakeDamage(float damage, Vector3 contact)
        {
            currentHealth -= damage;

            if (currentHealth <= 0f)
            {
                currentHealth = 0f;

                Killed();
            }
        }

        private void Killed()
        {
            CancelInvoke();

            Disappear();
        }

        private void Disappear()
        {
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