using System;

using System.Collections.Generic;

using System.Linq;

using ZL.Unity.Collections;

namespace ZL.Unity.Pooling
{
    [Serializable]

    public sealed class ManagedObjectPool<TKey> : ObjectPool<ManagedPooledObject<TKey>>
    {
        private readonly Dictionary<TKey, ManagedPooledObject<TKey>> clones = new Dictionary<TKey, ManagedPooledObject<TKey>>();

        public ManagedPooledObject<TKey> this[TKey key]
        {
            get => clones[key];
        }

        public bool TryGenerate(TKey key, out ManagedPooledObject<TKey> clone)
        {
            if (clones.ContainsKey(key) == true)
            {
                clone = clones[key];

                return false;
            }

            clone = Cloning();

            clone.Key = key;

            clones.Add(key, clone);

            return true;
        }

        public ManagedPooledObject<TKey> Find(TKey key)
        {
            return clones[key];
        }

        public override void Collect(ManagedPooledObject<TKey> pooledObject)
        {
            clones.Remove(pooledObject.Key);

            base.Collect(pooledObject);
        }

        public void CollectAll()
        {
            foreach (var pooledObject in clones.Values.ToArray())
            {
                pooledObject.gameObject.SetActive(false);
            }

            clones.Clear();
        }
    }

    [Serializable]

    public class ManagedObjectPool : ObjectPool<PooledObject>
    {
        private readonly HashSet<PooledObject> clones = new HashSet<PooledObject>();

        public override PooledObject Cloning()
        {
            var clone = base.Cloning();

            clones.Add(clone);

            return clone;
        }

        public override void Collect(PooledObject clone)
        {
            clones.Remove(clone);

            base.Collect(clone);
        }

        public void CollectAll()
        {
            foreach (var clone in clones.ToArray())
            {
                clone.gameObject.SetActive(false);
            }

            clones.Clear();
        }
    }
}