using UnityEngine;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Gathering (Spawned)")]

    public sealed class Gathering : SpawnedObject, IDamageable
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private GatheringData gatheringData = null;

        [SerializeField] GameObject gatheringVFX;

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
                if(gatheringVFX != null)
                {
                    gatheringVFX.SetActive(true);
                    gatheringVFX.transform.SetParent(null, true);
                }
                Disappear();
            }
        }
    }
}