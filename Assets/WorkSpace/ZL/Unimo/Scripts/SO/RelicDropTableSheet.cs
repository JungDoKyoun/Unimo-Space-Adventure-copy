using UnityEngine;

using ZL.Unity.IO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/Relic Drop Table Sheet (Singleton)", fileName = "Relic Drop Table Sheet")]

    public sealed class RelicDropTableSheet : ScriptableGoogleSheet<RelicDropTableSheet, RelicDropTable>
    {

    }
}