using System;

using System.Collections;

using System.Collections.Generic;

using UnityEngine;

namespace ZL.Unity.Collections
{
    [Serializable]

    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField]

        protected List<SerializableKeyValuePair<TKey, TValue>> elements = new List<SerializableKeyValuePair<TKey, TValue>>();

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {

        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            if (elements != null)
            {
                Clear();

                foreach (var element in elements)
                {
                    TryAdd(element.Key, element.Value);
                }
            }

            #if !UNITY_EDITOR

            elements = null;

            #endif
        }

        #if UNITY_EDITOR

        public new void Add(TKey key, TValue value)
        {
            base.Add(key, value);

            elements.Add(new SerializableKeyValuePair<TKey, TValue>(key, value));
        }

        #endif
    }

    [Serializable]

    public class SerializableDictionary<TKey, TValue, TKeyValuePair> : IEnumerable<TKeyValuePair>, ISerializationCallbackReceiver

        where TKeyValuePair : IKeyValuePair<TKey, TValue>
    {
        [SerializeField]

        private List<TKeyValuePair> elements = new List<TKeyValuePair>();

        private readonly Dictionary<TKey, TKeyValuePair> dictionary = new Dictionary<TKey, TKeyValuePair>();

        public TValue this[TKey key]
        {
            get => dictionary[key].Value;

            set => dictionary[key].Value = value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<TKeyValuePair> GetEnumerator()
        {
            return dictionary.Values.GetEnumerator();
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {

        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            if (elements != null)
            {
                dictionary.Clear();

                foreach (var element in elements)
                {
                    if (element != null)
                    {
                        dictionary.TryAdd(element.Key, element);
                    }
                }
            }

            #if !UNITY_EDITOR

            elements = null;

            #endif
        }

        public void Add(TKeyValuePair element)
        {
            dictionary.Add(element.Key, element);

            #if UNITY_EDITOR

            elements.Add(element);

            #endif
        }
        public void Clear()
        {
            dictionary.Clear();

            #if UNITY_EDITOR

            elements.Clear();

            #endif
        }

        public TKeyValuePair GetContainer(TKey key)
        {
            return dictionary[key];
        }
    }
}