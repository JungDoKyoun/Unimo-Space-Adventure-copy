using System;

using UnityEngine;

namespace ZL.Unity.Pooling
{
    [AddComponentMenu("ZL/Pooling/Pooled Object")]

    public class PooledObject : MonoBehaviour
    {
        public event Action OnDisappearedAction = null;

        private event Action OnCollectedAction = null;

        public static TClone Instantiate<TClone>(ObjectPool<TClone> objectPool)

            where TClone : PooledObject
        {
            var clone = Instantiate(objectPool.Prefab, objectPool.Parent);

            clone.OnCollectedAction += () => objectPool.Collect(clone);

            return clone;
        }

        public virtual void Appear()
        {
            gameObject.SetActive(true);
        }

        public virtual void Disappear()
        {
            gameObject.SetActive(false);

            OnDisappeared();
        }

        public virtual void OnDisappeared()
        {
            if (OnDisappearedAction != null)
            {
                OnDisappearedAction.Invoke();

                OnDisappearedAction = null;
            }

            OnCollectedAction?.Invoke();
        }
    }
}