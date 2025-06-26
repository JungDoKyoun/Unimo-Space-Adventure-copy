using System;

using System.Collections.Generic;

using System.Linq;

namespace ZL.Unity.Pooling
{
    [Serializable]

    public sealed class DictionaryObjectPool<TKey> : DictionaryObjectPool<TKey, ManagedPooledObject<TKey>>
    {

    }

    [Serializable]

    public class DictionaryObjectPool<TKey, TClone> : ObjectPool<TClone>
    
    where TClone : ManagedPooledObject<TKey>
    {
        private readonly Dictionary<TKey, TClone> clones = new();

        public TClone this[TKey key]
        {
            get => clones[key];
        }

        public bool TryGenerate(TKey key, out TClone clone)
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

        public override void Collect(TClone clone)
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
}