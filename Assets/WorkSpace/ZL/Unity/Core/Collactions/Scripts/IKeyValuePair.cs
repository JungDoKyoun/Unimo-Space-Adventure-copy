namespace ZL.Unity.Collections
{
    public interface IKeyValuePair<TKey, TValue>
    {
        public TKey Key { get; set; }

        public TValue Value { get; set; }

        public string ToString();
    }
}