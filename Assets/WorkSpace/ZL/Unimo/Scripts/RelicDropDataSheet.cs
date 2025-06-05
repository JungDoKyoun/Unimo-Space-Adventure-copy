using UnityEngine;

using ZL.Unity.IO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/Relic Drop Data Sheet (Singleton)", fileName = "Relic Drop Data Sheet (Singleton)")]

    public sealed class RelicDropDataSheet : ScriptableGoogleSheet<RelicDropDataSheet, RelicDropData>
    {

    }
}