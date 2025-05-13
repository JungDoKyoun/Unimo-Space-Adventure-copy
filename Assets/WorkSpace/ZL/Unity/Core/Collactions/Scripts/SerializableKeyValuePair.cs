using System;

using System.Collections.Generic;

using UnityEngine;

namespace ZL.Unity.Collections
{
    [Serializable]

    public class SerializableKeyValuePair<TKey, TValue> : IKeyValuePair<TKey, TValue>
    {
        [SerializeField]

        private TKey key;

        public TKey Key
        {
            get => key;

            set => key = value;
        }

        [SerializeField]

        private TValue value;

        public TValue Value
        {
            get => value;

            set => this.value = value;
        }

        public SerializableKeyValuePair(TKey key, TValue value)
        {
            this.key = key;

            this.value = value;
        }

        public SerializableKeyValuePair(KeyValuePair<TKey, TValue> keyValuePair)
        {
            key = keyValuePair.Key;

            value = keyValuePair.Value;
        }

        public override string ToString()
        {
            return $"[{key}, {value}]";
        }
    }
}