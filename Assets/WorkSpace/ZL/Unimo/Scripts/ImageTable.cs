using UnityEngine;

using ZL.Unity.Collections;

namespace ZL.Unity
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/Image Table", fileName = "Image Table")]

    public sealed class ImageTable : ScriptableObject
    {
        [Space]

        [SerializeField]

        private SerializableDictionary<string, Sprite> table = new SerializableDictionary<string, Sprite>();

        public Sprite this[string key]
        {
            get => table[key];
        }
    }
}