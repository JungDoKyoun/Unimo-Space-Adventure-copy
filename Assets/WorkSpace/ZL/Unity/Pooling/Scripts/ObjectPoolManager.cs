using UnityEngine;

using ZL.Unity.Collections;

using ZL.Unity.Singleton;

namespace ZL.Unity.Pooling
{
    [AddComponentMenu("ZL/Pooling/Object Pool Manager")]

    public class ObjectPoolManager : MonoSingleton<ObjectPoolManager>
    {
        [Space]

        [SerializeField]

        private SerializableDictionary<string, ObjectPool> poolDictionary = null;

        public TPooledObject Cloning<TPooledObject>(string key)

            where TPooledObject : PooledObject
        {
            return (TPooledObject)Cloning(key);
        }

        public PooledObject Cloning(string key)
        {
            return poolDictionary[key].Cloning();
        }
    }
}