using UnityEngine;

using ZL.Unity.SO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/Stage Data Sheet", fileName = "Stage Data Sheet")]

    public sealed class StageDataSheet : ScriptableGoogleSheet<string, StageData>
    {
        protected override string GetDataKey(StageData data)
        {
            return data.name;
        }
    }
}