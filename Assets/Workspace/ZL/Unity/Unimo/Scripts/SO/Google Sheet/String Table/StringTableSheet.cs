using UnityEngine;

using ZL.Unity.SO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/String Table Sheet", fileName = "String Table Sheet")]

    public sealed class StringTableSheet : ScriptableGoogleSheet<string, StringTable>
    {
        protected override string GetDataKey(StringTable data)
        {
            return data.name;
        }
    }
}