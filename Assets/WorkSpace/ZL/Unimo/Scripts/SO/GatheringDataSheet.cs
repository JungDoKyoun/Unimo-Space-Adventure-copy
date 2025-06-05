using UnityEngine;

using ZL.Unity.IO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/Gathering Data Sheet (Singleton)", fileName = "Gathering Data Sheet")]

    public sealed class GatheringDataSheet : ScriptableGoogleSheet<GatheringDataSheet, GatheringData>
    {

    }
}