using System;

using UnityEngine;

namespace ZL.Unity.Pooling
{
    [AddComponentMenu("ZL/Pooling/Pooled Object")]

    public class PooledObject : MonoBehaviour
    {
        private Action ReturnToPool = null;

        public static TClone Instantiate<TClone>(ObjectPool<TClone> pool)

            where TClone : Component
        {
            var clone = Instantiate(pool.Prefab, pool.Parent);

            if (clone.TryGetComponent<PooledObject>(out var pooledObject) == false)
            {
                FixedDebug.LogWarning($"Prefab '{pool.Prefab.name}' being pooled does not have a component of type 'Pooled Object'. We recommend adding it to the prefab to improve performance.");

                pooledObject = clone.AddComponent<PooledObject>();
            }

            pooledObject.ReturnToPool = () => pool.Collect(clone);

            return clone;
        }

        #if UNITY_EDITOR

        private void Start()
        {
            if (ReturnToPool == null)
            {
                FixedDebug.LogWarning($"Game Object '{gameObject.name}' is a 'Pooled Object' but was not created from an'Object Pool'.");
            }
        }

        #endif

        private void OnDisable()
        {
            ReturnToPool();
        }
    }
}