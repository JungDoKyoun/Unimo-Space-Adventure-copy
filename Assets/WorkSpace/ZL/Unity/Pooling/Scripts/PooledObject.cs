using System;

using UnityEngine;

namespace ZL.Unity.Pooling
{
    [AddComponentMenu("ZL/Pooling/Pooled Object")]

    public class PooledObject : MonoBehaviour
    {
        public event Action OnDisableAction = null;

        private event Action ReturnToPool = null;

        public static TClone Instantiate<TClone>(ObjectPool<TClone> pool)

            where TClone : Component
        {
            var clone = Instantiate(pool.Prefab, pool.Parent);

            var pooledObject = clone.GetComponent<PooledObject>();

            pooledObject.ReturnToPool = () => pool.Collect(clone);

            return clone;
        }

        #if UNITY_EDITOR

        protected virtual void Start()
        {
            if (ReturnToPool == null)
            {
                FixedDebug.LogWarning($"Game Object '{gameObject.name}' is a 'Pooled Object' but was not created from an 'Object Pool'.");
            }
        }

        #endif

        protected virtual void OnDisable()
        {
            OnDisableAction?.Invoke();

            OnDisableAction = null;

            ReturnToPool?.Invoke();
        }
    }
}