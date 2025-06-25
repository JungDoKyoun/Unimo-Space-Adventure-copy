using UnityEngine;

using UnityEngine.Animations;

using ZL.Unity.Pooling;

namespace ZL.Unity.Unimo
{
    public abstract class SpawnedObject : PooledObject
    {
        private float lifeTime = -1f;

        public float LifeTime
        {
            set => lifeTime = value;
        }

        private Vector3 spawnPosition = Vector3.zero;

        public Vector3 SpawnPosition
        {
            set => spawnPosition = value;
        }

        private float despawnRange = -1f;

        public float DespawnRange
        {
            set => despawnRange = value;
        }

        public virtual void OnAppeared()
        {
            if (lifeTime != -1f)
            {
                Invoke(nameof(Disappear), lifeTime);
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

            lifeTime = -1f;
        }

        protected virtual void CheckDespawnCondition()
        {
            if (despawnRange == -1f)
            {
                return;
            }

            if (IsWithinRange(spawnPosition, despawnRange) == true)
            {
                return;
            }

            Disappear();
        }

        protected bool IsWithinRange(Vector3 position, float distance)
        {
            return transform.position.DistanceTo(position, Axis.Y) <= distance;
        }
    }
}