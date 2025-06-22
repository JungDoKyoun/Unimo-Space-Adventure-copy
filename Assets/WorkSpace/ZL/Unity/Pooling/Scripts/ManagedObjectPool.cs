using System;

using System.Collections.Generic;

using System.Linq;

namespace ZL.Unity.Pooling
{
    [Serializable]

    public sealed class ManagedObjectPool<TKey> : ObjectPool<ManagedPooledObject<TKey>>
    {
        private readonly Dictionary<TKey, ManagedPooledObject<TKey>> clones = new();

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

            clone = Clone();

            clone.Key = key;

            clones.Add(key, clone);

            return true;
        }

        public ManagedPooledObject<TKey> Find(TKey key)
        {
            return clones[key];
        }

        public override void Collect(ManagedPooledObject<TKey> clone)
        {
            clones.Remove(clone.Key);

            base.Collect(clone);
        }

        public void CollectAll()
        {
            foreach (var clone in clones.Values.ToArray())
            {
                clone.Disappear();
            }

            clones.Clear();
        }
    }

    [Serializable]

    public class ManagedObjectPool : ObjectPool<PooledObject>
    {
        private readonly HashSet<PooledObject> clones = new();

        public override PooledObject Clone()
        {
            var clone = base.Clone();

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
                clone.Disappear();
            }

            clones.Clear();
        }
    }
}