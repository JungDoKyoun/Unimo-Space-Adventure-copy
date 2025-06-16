using System;

using UnityEngine;

using ZL.CS.Pooling;

namespace ZL.Unity.Pooling
{
    [Serializable]

    public class ObjectPool : ObjectPool<PooledObject>
    {

    }

    public abstract class ObjectPool<TClone> : Pool<TClone>

        where TClone : PooledObject
    {
        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        protected TClone prefab = null;

        public TClone Prefab
        {
            get => prefab;
        }

        [SerializeField]

        private Transform parent = null;

        public Transform Parent
        {
            get => parent;
        }

        public override TClone Instantiate()
        {
            return PooledObject.Instantiate(this);
        }
    }
}