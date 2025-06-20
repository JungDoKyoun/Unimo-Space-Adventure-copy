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

        private float currentHealth = 0f;

        public float CurrentHealth
        {
            get => currentHealth;
        }

        public override void Appear()
        {
            currentHealth = gatheringData.MaxHealth;

            base.Appear();
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
    }
}