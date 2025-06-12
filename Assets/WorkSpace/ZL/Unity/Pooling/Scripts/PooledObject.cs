using System;

using UnityEngine;

namespace ZL.Unity.Pooling
{
    [AddComponentMenu("ZL/Pooling/Pooled Object")]

    public class PooledObject : MonoBehaviour
    {
        public event Action OnDisableAction = null;

        private event Action OnCollectedAction = null;

        public static TPooledObject Instantiate<TPooledObject>(ObjectPool<TPooledObject> objectPool)

            where TPooledObject : PooledObject
        {
            var clone = Instantiate(objectPool.Prefab, objectPool.Parent);

            clone.OnCollectedAction += () => objectPool.Collect(clone);

            return clone;
        }

        #if UNITY_EDITOR

        protected virtual void Start()
        {
            if (OnCollectedAction == null)
            {
                FixedDebug.LogWarning($"Game Object '{gameObject.name}' is a 'Pooled Object' but was not created from an 'Object Pool'.");
            }
        }

        #endif

        protected virtual void OnDisable()
        {
            OnDisableAction?.Invoke();

            OnDisableAction = null;

            OnCollectedAction?.Invoke();
        }
    }
}