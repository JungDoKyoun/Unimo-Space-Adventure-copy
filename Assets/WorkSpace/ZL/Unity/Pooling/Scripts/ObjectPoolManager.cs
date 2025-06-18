using UnityEngine;

using ZL.Unity.Collections;

using ZL.Unity.Singleton;

namespace ZL.Unity.Pooling
{
    [AddComponentMenu("ZL/Pooling/Object Pool Manager (Singleton)")]

    public class ObjectPoolManager : MonoSingleton<ObjectPoolManager>
    {
        [Space]

        [SerializeField]

        private SerializableDictionary<string, ObjectPool> poolDictionary = null;

        public TClone Clone<TClone>(string key)

            where TClone : PooledObject
        {
            return (TClone)Clone(key);
        }

        public PooledObject Clone(string key)
        {
            return poolDictionary[key].Clone();
        }
    }
}