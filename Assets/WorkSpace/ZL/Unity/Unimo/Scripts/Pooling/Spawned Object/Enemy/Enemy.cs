using UnityEngine;

using ZL.Unity.Animating;

namespace ZL.Unity.Unimo
{
    public abstract class Enemy : SpawnedObject, IDamageable
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

        [ReadOnlyWhenPlayMode]

        protected AnimatorGroup animatorGroup = null;

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

        public override void Appear()
        {
            currentHealth = enemyData.MaxHealth;

            if (spawner != null)
            {
                rotationSpeed = spawner.RotationSpeed;
            }

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
            if (animatorGroup != null)
            {
                animatorGroup.SetTrigger("Disappear");
            }
        }

        public override void OnDisappeared()
        {
            base.OnDisappeared();

            rigidbody.velocity = Vector3.zero;

            if (animatorGroup != null)
            {
                animatorGroup.Rebind();
            }

            rotationSpeed = -1f;
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

        protected virtual void Killed()
        {
            Disappear();
        }
    }
}