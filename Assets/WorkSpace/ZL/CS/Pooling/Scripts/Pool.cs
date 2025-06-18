using System.Collections.Generic;

using ZL.CS.Collections;

namespace ZL.CS.Pooling
{
    public abstract class Pool<TClone>

        where TClone : class
    {
        protected readonly LinkedList<TClone> collection = new();

        public virtual TClone Clone()
        {
            if (collection.Count != 0)
            {
                return collection.PopLast();
            }

            return Instantiate();
        }

        public abstract TClone Instantiate();

        public virtual void Collect(TClone clone)
        {
            collection.AddLast(clone);
        }
    }
}