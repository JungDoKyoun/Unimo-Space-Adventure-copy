using UnityEngine;

using ZL.Unity.IO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/Stage Reward Data Sheet", fileName = "Stage Reward Data Sheet")]

    public sealed class StageRewardDataSheet : ScriptableGoogleSheet<StageRewardData>
    {

    }
}