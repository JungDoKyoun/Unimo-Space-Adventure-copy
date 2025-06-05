using UnityEngine;

using ZL.Unity.IO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/String Table Sheet (Singleton)", fileName = "String Table Sheet")]

    public sealed class StringTableSheet : ScriptableGoogleSheet<StringTableSheet, StringTable>
    {

    }
}