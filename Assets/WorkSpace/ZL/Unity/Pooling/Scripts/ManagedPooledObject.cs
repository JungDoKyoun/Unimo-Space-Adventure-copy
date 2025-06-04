using ZL.Unity.Collections;

namespace ZL.Unity.Pooling
{
    public abstract class ManagedPooledObject<TKey> : PooledObject, IKeyValuePair<TKey, ManagedPooledObject<TKey>>
    {
        private TKey key;

        public TKey Key
        {
            get => key;

            set => key = value;
        }

        public ManagedPooledObject<TKey> Value
        {
            get => this;

            set { }
        }
    }
}