using UnityEngine;

using ZL.Unity.Pooling;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Gathering (Pooled)")]

    public sealed class Gathering : PooledObject, IDamageable
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private GatheringData gatheringData = null;

        public GatheringData GatheringData
        {
            get => gatheringData;
        }

        [SerializeField]

        private float lifeTime = -1f;

        private float currentHealth = 0f;

        public float CurrentHealth
        {
            get => currentHealth;
        }

        private void OnEnable()
        {
            if (lifeTime > 0f)
            {
                Invoke(nameof(Disappear), lifeTime);
            }

            currentHealth = gatheringData.MaxHealth;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            OnDisappear();
        }

        public void TakeDamage(float damage, Vector3 contact = default)
        {
            currentHealth -= damage;

            if (currentHealth <= 0f)
            {
                currentHealth = 0f;

                ++GatheringManager.Instance.GatheringCount;

                Disappear();
            }
        }

        private void Disappear()
        {
            CancelInvoke(nameof(Disappear));

            OnDisappear();

            OnDisappeared();
        }

        private void OnDisappear()
        {

        }

        public void OnDisappeared()
        {
            gameObject.SetActive(false);
        }
    }
}