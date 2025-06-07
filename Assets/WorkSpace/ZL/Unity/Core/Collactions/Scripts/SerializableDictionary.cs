using System;

using System.Collections;

using System.Collections.Generic;

using UnityEngine;

namespace ZL.Unity.Collections
{
    [Serializable]

    public sealed class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver

        where TKey : IComparable, IComparable<TKey>
    {
        [SerializeField]

        private List<SerializableKeyValuePair<TKey, TValue>> items = new List<SerializableKeyValuePair<TKey, TValue>>();

        public new void Add(TKey key, TValue value)
        {
            base.Add(key, value);

            items.Add(new SerializableKeyValuePair<TKey, TValue>(key, value));
        }

        public void OnBeforeSerialize()
        {
            #if !UNITY_EDITOR

            Serialize();

            #endif
        }

        public void Serialize()
        {
            items.Clear();

            foreach (var item in this)
            {
                items.Add(new SerializableKeyValuePair<TKey, TValue>(item));
            }
        }

        public void OnAfterDeserialize()
        {
            Deserialize();
        }

        public void Deserialize()
        {
            Clear();

            foreach (var item in items)
            {
                TryAdd(item.Key, item.Value);
            }
        }
    }

    [Serializable]

    public sealed class SerializableDictionary<TKey, TValue, TKeyValuePair> : IEnumerable<TKeyValuePair>, ISerializationCallbackReceiver

        where TKeyValuePair : IKeyValuePair<TKey, TValue>
    {
        private readonly Dictionary<TKey, TKeyValuePair> @base = new Dictionary<TKey, TKeyValuePair>();

        [SerializeField]

        private List<TKeyValuePair> items = new List<TKeyValuePair>();

        public TValue this[TKey key]
        {
            get => @base[key].Value;

            set => @base[key].Value = value;
        }

        public void Add(TKeyValuePair item)
        {
            @base.Add(item.Key, item);

            items.Add(item);
        }

        public void Remove(TKey key)
        {
            @base.Remove(key);

            #if UNITY_EDITOR

            Serialize();

            #endif
        }

        public void Clear()
        {
            @base.Clear();

            items.Clear();
        }

        public TKeyValuePair GetItem(TKey key)
        {
            return @base[key];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<TKeyValuePair> GetEnumerator()
        {
            return @base.Values.GetEnumerator();
        }

        public void OnBeforeSerialize()
        {
            #if !UNITY_EDITOR

            Serialize();

            #endif
        }

        public void Serialize()
        {
            items.Clear();

            foreach (var item in this)
            {
                items.Add(item);
            }
        }

        public void OnAfterDeserialize()
        {
            Deserialize();
        }

        public void Deserialize()
        {
            @base.Clear();

            foreach (var item in items)
            {
                if (item != null)
                {
                    @base.TryAdd(item.Key, item);
                }
            }
        }
    }
}