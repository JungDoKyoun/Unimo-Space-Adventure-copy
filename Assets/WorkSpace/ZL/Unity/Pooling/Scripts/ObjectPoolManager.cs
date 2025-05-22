using UnityEngine;
using UnityEngine.Serialization;
using ZL.Unity.Collections;

using ZL.Unity.Singleton;

namespace ZL.Unity.Pooling
{
    [AddComponentMenu("ZL/Pooling/Object Pool Manager")]

    public sealed class ObjectPoolManager : ObjectPoolManager<ObjectPoolManager, PooledObject>
    {

    }

    public abstract class ObjectPoolManager<TMonoSingleton, TClone> : MonoSingleton<TMonoSingleton>

        where TMonoSingleton : MonoSingleton<TMonoSingleton>

        where TClone : Component
    {
        [Space]

        [SerializeField]

        protected SerializableDictionary<string, ObjectPool<TClone>> poolDictionary = null;

        public TClone Cloning(string key)
        {
            return poolDictionary[key].Cloning();
        }
    }
}