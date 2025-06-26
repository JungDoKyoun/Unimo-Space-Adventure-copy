using System;

using System.Collections.Generic;

using System.Linq;

namespace ZL.Unity.Pooling
{
    [Serializable]

    public sealed class HashSetObjectPool : HashSetObjectPool<PooledObject>
    {

    }
    
    [Serializable]

    public class HashSetObjectPool<TClone> : ObjectPool<TClone>

        where TClone : PooledObject
    {
        private readonly HashSet<TClone> clones = new();

        public HashSet<TClone> Clones
        {
            get => clones;
        }

        public override TClone Clone()
        {
            var clone = base.Clone();

            clones.Add(clone);

            return clone;
        }

        public override void Collect(TClone clone)
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