using UnityEngine;

using ZL.CS.Singleton;

using ZL.Unity.IO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/Reward Data Sheet (Singleton)", fileName = "Reward Data Sheet")]

    public sealed class RewardDataSheet : ScriptableGoogleSheet<RewardData>, ISingleton<RewardDataSheet>
    {

    }
}