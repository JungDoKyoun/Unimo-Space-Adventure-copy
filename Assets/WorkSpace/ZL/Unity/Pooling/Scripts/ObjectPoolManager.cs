using UnityEngine;

using ZL.Unity.Collections;

using ZL.Unity.Singleton;

namespace ZL.Unity.Pooling
{
    [AddComponentMenu("ZL/Pooling/Object Pool Manager")]

    public sealed class ObjectPoolManager : ObjectPoolManager<ObjectPoolManager, Transform>
    {

    }

    public abstract class ObjectPoolManager<TMonoSingleton, TClone> : MonoSingleton<TMonoSingleton>

        where TMonoSingleton : MonoSingleton<TMonoSingleton>

        where TClone : Component
    {
        [Space]

        [SerializeField]

        protected SerializableDictionary<string, ObjectPool<TClone>> objectPoolDictionary = null;

        public virtual TClone Cloning(string key)
        {
            return objectPoolDictionary[key].Cloning();
        }
    }
}