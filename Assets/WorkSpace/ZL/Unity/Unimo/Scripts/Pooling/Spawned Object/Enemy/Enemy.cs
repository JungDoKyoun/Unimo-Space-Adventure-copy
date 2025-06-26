using System;

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

        private Collider mainCollider = null;

        public Collider MainCollider
        {
            get => mainCollider;
        }

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        #pragma warning disable CS0108

        protected Rigidbody rigidbody = null;

        #pragma warning restore CS0108

        [SerializeField]

        [UsingCustomProperty]

        [GetComponentInChildren]

        [Essential]

        [ReadOnly(true)]

        protected AnimatorGroup animatorGroup = null;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        protected EnemyData enemyData = null;

        public EnemyData EnemyData
        {
            get => enemyData;
        }

        [SerializeField]

        private StringTable enemyNameTable = null;

        public StringTable EnemyNameTable
        {
            get => enemyNameTable;
        }

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
            set => movementSpeedMultiplier = value;
        }

        private float currentHealth = 0f;

        public virtual float CurrentHealth
        {
            get => currentHealth;

            set
            {
                currentHealth = Math.Clamp(value, 0f, enemyData.MaxHealth);

                OnHealthChangedAction?.Invoke(currentHealth);

                if (currentHealth == 0f)
                {
                    OnKiiledAction?.Invoke();

                    Kill();
                }
            }
        }

        protected float rotationSpeed = 0f;

        protected float movementSpeed = 0f;

        private EnemyManager enemyManager = null;

        protected virtual Transform Destination
        {
            get => enemyManager.Destination;
        }

        protected bool isStoped = true;

        public event Action<float> OnHealthChangedAction = null;

        public event Action OnKiiledAction = null;

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

        protected virtual void OnEnable()
        {
            animatorGroup.SetFloat(nameof(movementSpeedMultiplier), movementSpeedMultiplier);
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
            mainCollider.enabled = true;

            isStoped = false;

            base.OnAppeared();
        }

        public override void Disappear()
        {
            base.Disappear();

            mainCollider.enabled = false;

            isStoped = true;

            OnHealthChangedAction = null;
            
            OnKiiledAction = null;
        }

        protected override void OnDisappear()
        {
            animatorGroup.SetTrigger("Disappear");
        }

        public override void OnDisappeared()
        {
            base.OnDisappeared();

            rigidbody.velocity = Vector3.zero;

            animatorGroup.Rebind();

            rotationSpeedMultiplier = 1f;

            movementSpeedMultiplier = 1f;
        }

        public virtual void TakeDamage(float damage, Vector3 contact)
        {
            CurrentHealth -= damage;
        }

        protected virtual void Kill()
        {
            Disappear();
        }
    }
}