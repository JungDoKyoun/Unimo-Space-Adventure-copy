using UnityEngine;

using ZL.CS.Singleton;

using ZL.Unity.SO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/Relic Drop Table Sheet (Singleton)", fileName = "Relic Drop Table Sheet")]

    public sealed class RelicDropTableSheet : ScriptableGoogleSheet<string, RelicDropTable>, ISingleton<RelicDropTableSheet>
    {
        public static RelicDropTableSheet Instance
        {
            get => ISingleton<RelicDropTableSheet>.Instance;
        }

        protected override string GetDataKey(RelicDropTable data)
        {
            return data.name;
        }
    }
}