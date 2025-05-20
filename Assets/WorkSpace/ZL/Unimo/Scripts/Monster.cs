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

        [GetComponentInChildren]

        [ReadOnly(true)]

        private Animator animator = null;

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

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        protected MonsterData monsterData = null;

        [SerializeField]

        protected float lookSpeed = 0f;

        protected float currentHealth = 0f;

        public float CurrentHealth
        {
            get => currentHealth;
        }

        protected virtual void OnEnable()
        {
            if (MonsterManager.Instance.Target != null)
            {
                rigidbody.rotation = rigidbody.LookRotation(MonsterManager.Instance.Target, Axis.Y);
            }

            currentHealth = monsterData.MaxHealth;
        }

        private void OnDisable()
        {
            if (animator != null)
            {
                animator.Rebind();
            }

            if (rigidbody != null)
            {
                rigidbody.velocity = Vector3.zero;
            }
        }

        public virtual void TakeDamage(float damage, Vector3 contact)
        {
            currentHealth -= damage;

            if (currentHealth <= 0f)
            {
                currentHealth = 0f;

                Dead();
            }
        }

        public virtual void Dead()
        {
            gameObject.SetActive(false);
        }
    }
}