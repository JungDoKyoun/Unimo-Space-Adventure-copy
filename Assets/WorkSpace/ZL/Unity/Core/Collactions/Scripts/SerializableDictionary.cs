using System;

using System.Collections;

using System.Collections.Generic;

using UnityEngine;

namespace ZL.Unity.Collections
{
    [Serializable]

    public sealed class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField]

        private List<SerializableKeyValuePair<TKey, TValue>> items = new();

        public new void Add(TKey key, TValue value)
        {
            base.Add(key, value);

            items.Add(new(key, value));
        }

        public new void Clear()
        {
            base.Clear();

            items.Clear();
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
                items.Add(new(item));
            }
        }

        public void OnAfterDeserialize()
        {
            Deserialize();
        }

        public void Deserialize()
        {
            base.Clear();

            foreach (var item in items)
            {
                if (item == null)
                {
                    continue;
                }

                TryAdd(item.Key, item.Value);
            }
        }
    }

    [Serializable]

    public sealed class SerializableDictionary<TKey, TValue, TKeyValuePair> : IEnumerable<TKeyValuePair>, ISerializationCallbackReceiver

        where TKeyValuePair : IKeyValuePair<TKey, TValue>, new()
    {
        private readonly Dictionary<TKey, TKeyValuePair> @base = new();

        [SerializeField]

        private List<TKeyValuePair> items = new();

        public TValue this[TKey key]
        {
            get => @base[key].Value;

            set => @base[key].Value = value;
        }

        public void Add(TKey key, TValue value)
        {
            Add(new() { Key = key, Value = value });
        }

        public void Add(TKeyValuePair item)
        {
            @base.Add(item.Key, item);

            items.Add(item);
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
                if (item == null)
                {
                    continue;
                }

                @base.TryAdd(item.Key, item);
            }
        }
    }
}