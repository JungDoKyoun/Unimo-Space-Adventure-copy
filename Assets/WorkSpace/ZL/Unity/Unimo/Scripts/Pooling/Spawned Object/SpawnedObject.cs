using UnityEngine;

using UnityEngine.Animations;

using ZL.Unity.Pooling;

namespace ZL.Unity.Unimo
{
    public abstract class SpawnedObject : PooledObject
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Button(nameof(Appear))]

        [Button(nameof(Disappear))]

        [Margin]

        [ReadOnly(true)]

        protected Spawner spawner = null;

        public Spawner Spawner
        {
            set => spawner = value;
        }

        public virtual void OnAppeared()
        {
            if (spawner == null)
            {
                return;
            }

            if (spawner.LifeTime != -1f)
            {
                Invoke(nameof(Disappear), spawner.LifeTime);
            }
        }

        public override void Disappear()
        {
            CancelInvoke(nameof(Disappear));

            OnDisappear();
        }

        protected virtual void OnDisappear()
        {
            OnDisappeared();
        }

        public virtual void OnDisappeared()
        {
            base.Disappear();

            if (spawner == null)
            {
                return;
            }

            spawner.Despawn();

            spawner = null;
        }

        protected void CheckDistanceToSpawner()
        {
            if (spawner == null)
            {
                return;
            }

            if (spawner.DespawnDistance == -1f)
            {
                return;
            }

            float distanceToSpawner = transform.position.DistanceTo(spawner.transform.position, Axis.Y);

            if (distanceToSpawner > spawner.DespawnDistance)
            {
                Disappear();
            }
        }
    }
}