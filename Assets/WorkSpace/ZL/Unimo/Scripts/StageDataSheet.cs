using UnityEngine;

using ZL.Unity.IO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/Stage Data Sheet", fileName = "Stage Data Sheet")]

    public sealed class StageDataSheet : ScriptableGoogleSheet<GatherStageData>
    {

    }
}