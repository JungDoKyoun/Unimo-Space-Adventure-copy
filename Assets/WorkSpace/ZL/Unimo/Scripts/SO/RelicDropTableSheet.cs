using UnityEngine;

using ZL.CS.Singleton;

using ZL.Unity.IO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/Relic Drop Table Sheet (Singleton)", fileName = "Relic Drop Table Sheet")]

    public sealed class RelicDropTableSheet : ScriptableGoogleSheet<RelicDropTable>, ISingleton<RelicDropTableSheet>
    {
        public static RelicDropTableSheet Instance
        {
            get => ISingleton<RelicDropTableSheet>.Instance;
        }
    }
}