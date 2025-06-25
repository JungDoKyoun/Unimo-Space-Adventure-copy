using UnityEngine;

using UnityEngine.Animations;

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

        [Space]

        [SerializeField]

        protected float rotationSpeedMultiplier = 1f;

        public float RotationSpeedMultiplier
        {
            set => rotationSpeedMultiplier = value;
        }

        [SerializeField]

        protected float movementSpeedMultiplier = 1f;

        public float MovementSpeedMultiplier
        {
            set
            {
                movementSpeedMultiplier = value;

                animatorGroup.SetFloat("movementSpeedMultiplier", movementSpeedMultiplier);
            }
        }

        protected float currentHealth = 0f;

        public float CurrentHealth
        {
            get => currentHealth;
        }

        protected float rotationSpeed = 0f;

        protected float movementSpeed = 0f;

        private EnemyManager enemyManager = null;

        protected virtual Transform Destination
        {
            get => enemyManager.Destination;
        }

        protected bool isStoped = true;

        private void OnValidate()
        {
            if (Application.isPlaying == false)
            {
                return;
            }

            //RotationSpeedMultiplier = rotationSpeedMultiplier;

            //MovementSpeedMultiplier = movementSpeedMultiplier;
        }

        protected virtual void Awake()
        {
            enemyManager = EnemyManager.Instance;
        }

        protected virtual void FixedUpdate()
        {
            if (isStoped == true)
            {
                return;
            }

            Look();

            Move();

            CheckDespawnCondition();
        }

        protected virtual void Look()
        {
            float rotationSpeed = this.rotationSpeed * rotationSpeedMultiplier;

            if (rotationSpeed != 0f)
            {
                rigidbody.LookTowards(Destination.position, enemyData.RotationSpeed * Time.fixedDeltaTime, Axis.Y);
            }
        }

        protected virtual void Move()
        {
            float movementSpeed = this.movementSpeed * movementSpeedMultiplier;

            if (movementSpeed != 0f)
            {
                rigidbody.MoveForward(movementSpeed * Time.fixedDeltaTime);

                animatorGroup.SetBool("isMoving", true);
            }

            else
            {
                animatorGroup.SetBool("isMoving", false);
            }
        }

        public override void Appear()
        {
            currentHealth = enemyData.MaxHealth;

            rotationSpeed = enemyData.RotationSpeed;

            movementSpeed = enemyData.MovementSpeed;

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

            rotationSpeedMultiplier = 1f;

            movementSpeedMultiplier = 1f;
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