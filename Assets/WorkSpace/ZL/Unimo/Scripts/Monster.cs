using UnityEngine;

using UnityEngine.Animations;

using ZL.Unity.Phys;

namespace ZL.Unity.Unimo
{
    public abstract class Monster : MonoBehaviour, IDamageable
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

        [GetComponentInChildren]

        [ReadOnly(true)]

        protected Animator animator = null;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        protected MonsterData monsterData = null;

        [SerializeField]

        protected float rotationSpeed = 0f;

        protected float currentHealth = 0f;

        public float CurrentHealth
        {
            get => currentHealth;
        }

        private Transform target = null;

        protected Transform Target
        {
            get => target;
        }

        private void OnEnable()
        {
            if (rigidbody != null)
            {
                rigidbody.velocity = Vector3.zero;
            }

            if (animator != null)
            {
                animator.Rebind();
            }

            currentHealth = monsterData.MaxHealth;
        }

        public void FindTarget()
        {
            target = MonsterManager.Instance.Target;
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

        private void Disappear()
        {
            target = null;

            animator.SetTrigger("Disappear");
        }
    }
}