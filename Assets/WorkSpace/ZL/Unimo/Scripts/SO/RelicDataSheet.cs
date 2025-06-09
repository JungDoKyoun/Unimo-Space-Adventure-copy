using UnityEngine;

using ZL.CS.Singleton;

using ZL.Unity.IO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/Relic Data Sheet", fileName = "Relic Data Sheet")]

    public sealed class RelicDataSheet : ScriptableGoogleSheet<RelicData>, ISingleton<RelicDataSheet>
    {

    }
}