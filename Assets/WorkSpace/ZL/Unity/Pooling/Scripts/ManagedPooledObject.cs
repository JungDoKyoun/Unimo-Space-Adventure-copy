using ZL.Unity.Collections;

namespace ZL.Unity.Pooling
{
    public abstract class ManagedPooledObject<TKey, TValue> : PooledObject, IKeyValuePair<TKey, TValue>

        where TValue : ManagedPooledObject<TKey, TValue>
    {
        private TKey key;

        public TKey Key
        {
            get => key;

            set => key = value;
        }

        private TValue value;

        public TValue Value
        {
            get => value;

            set
            {

            }
        }

        private void Awake()
        {
            value = (TValue)this;
        }
    }
}