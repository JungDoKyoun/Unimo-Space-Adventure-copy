using UnityEngine;

using ZL.CS.Singleton;

using ZL.Unity.IO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/Stage Reward Data Sheet (Singleton)", fileName = "Stage Reward Data Sheet")]

    public sealed class StageRewardDataSheet : ScriptableGoogleSheet<StageRewardData>, ISingleton<StageRewardDataSheet>
    {

    }
}