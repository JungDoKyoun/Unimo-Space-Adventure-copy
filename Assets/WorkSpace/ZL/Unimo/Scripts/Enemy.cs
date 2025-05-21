using UnityEngine;

using UnityEngine.Animations;

using ZL.Unity.Phys;

namespace ZL.Unity.Unimo
{
    public abstract class Enemy : MonoBehaviour, IDamageable
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [ReadOnly(true)]

        #pragma warning disable CS0108

        protected Rigidbody rigidbody = null;

        #pragma warning restore CS0108

        public Rigidbody Rigidbody
        {
            get => rigidbody;
        }

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [ReadOnly(true)]

        #pragma warning disable CS0108

        private Collider collider = null;

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

        protected Transform Target
        {
            get => EnemyManager.Instance.Target;
        }

        protected bool isStoped = true;

        protected virtual void OnEnable()
        {
            if (Target != null)
            {
                var forward = Target.position - transform.position;

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

        public void OnAppeared()
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
            collider.enabled = false;

            isStoped = true;

            animator.SetTrigger("Disappear");
        }

        public void OnDisappeared()
        {
            gameObject.SetActive(false);
        }
    }
}