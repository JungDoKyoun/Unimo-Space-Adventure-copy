using System;

using UnityEngine;

namespace ZL.Unity.Pooling
{
    [AddComponentMenu("ZL/Pooling/Pooled Object")]

    public class PooledObject : MonoBehaviour
    {
        public event Action OnDisableAction = null;

        private event Action OnCollectedAction = null;

        public static TClone Instantiate<TClone>(ObjectPool<TClone> objectPool)

            where TClone : PooledObject
        {
            var clone = Instantiate(objectPool.Prefab, objectPool.Parent);

            clone.OnCollectedAction += () => objectPool.Collect(clone);

            return clone;
        }

        protected virtual void OnDisable()
        {
            if (OnDisableAction != null)
            {
                OnDisableAction.Invoke();

                OnDisableAction = null;
            }

            if (OnCollectedAction != null)
            {
                OnCollectedAction.Invoke();
            }

            #if UNITY_EDITOR

            else
            {
                FixedDebug.LogWarning($"Game Object '{gameObject.name}' is a 'Pooled Object' but was not created from an 'Object Pool'.");
            }

            #endif
        }
    }
}