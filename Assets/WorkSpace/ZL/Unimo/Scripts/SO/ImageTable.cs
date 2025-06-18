using UnityEngine;

using ZL.Unity.Collections;

namespace ZL.Unity
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/Image Table", fileName = "Image Table")]

    public sealed class ImageTable : ScriptableObject
    {
        [Space]

        [SerializeField]

        private Sprite[] datas;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Button(nameof(SerializeDatas))]

        private SerializableDictionary<string, Sprite> dataDictionary = new();

        public Sprite this[string key]
        {
            get => dataDictionary[key];
        }

        public void SerializeDatas()
        {
            dataDictionary.Clear();

            for (int i = 0; i < datas.Length; ++i)
            {
                var data = datas[i];

                dataDictionary.Add(data.name, data);
            }

            FixedEditorUtility.SetDirty(this);
        }
    }
}