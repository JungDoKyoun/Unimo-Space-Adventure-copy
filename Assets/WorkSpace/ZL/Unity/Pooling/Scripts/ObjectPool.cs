using System;

using UnityEngine;

using ZL.CS.Pooling;

namespace ZL.Unity.Pooling
{
    [Serializable]

    public class ObjectPool<TClone> : Pool<TClone>

        where TClone : Component
    {
        [SerializeField]

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