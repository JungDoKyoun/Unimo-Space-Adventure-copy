using UnityEngine;

using ZL.Unity.IO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/Spawn Pattern Data Sheet", fileName = "Spawn Pattern Data Sheet")]

    public sealed class SpawnPatternDataSheet : ScriptableGoogleSheet<SpawnPatternData>
    {

    }
}