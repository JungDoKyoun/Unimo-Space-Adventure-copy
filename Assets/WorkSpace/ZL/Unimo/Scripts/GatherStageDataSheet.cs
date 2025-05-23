using UnityEngine;

using ZL.Unity.IO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/Gather Stage Data Sheet", fileName = "Gather Stage Data Sheet")]

    public sealed class GatherStageDataSheet : ScriptableGoogleSheet<GatherStageData>
    {

    }
}