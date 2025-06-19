using UnityEngine;

using ZL.Unity.Collections;

namespace ZL.Unity.SO
{
    public abstract class ScriptableObjectDictionary<TKey, TValue> : ScriptableObject
    {
        [Space]

        [SerializeField]

        private TValue[] datas;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Button(nameof(Serialize))]

        private SerializableDictionary<TKey, TValue> dataDictionary = new();

        public TValue this[TKey key]
        {
            get => dataDictionary[key];
        }

        public virtual void Serialize()
        {
            dataDictionary.Clear();

            for (int i = 0; i < datas.Length; ++i)
            {
                var data = datas[i];

                dataDictionary.Add(GetDataKey(data), data);
            }

            FixedEditorUtility.SetDirty(this);
        }

        protected abstract TKey GetDataKey(TValue data);
    }
}