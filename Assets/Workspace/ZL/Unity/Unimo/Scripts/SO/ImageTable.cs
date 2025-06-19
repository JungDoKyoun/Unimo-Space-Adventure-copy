using UnityEngine;

using ZL.Unity.SO;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/Image Table", fileName = "Image Table")]

    public sealed class ImageTable : ScriptableObjectDictionary<string, Sprite>
    {
        protected override string GetDataKey(Sprite data)
        {
            return data.name;
        }
    }
}