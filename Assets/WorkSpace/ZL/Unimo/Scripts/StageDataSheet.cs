using UnityEngine;

using ZL.Unity.IO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/Stage Data Sheet (Singleton)", fileName = "Stage Data Sheet (Singleton)")]

    public sealed class StageDataSheet : ScriptableGoogleSheet<StageDataSheet, StageData>
    {

    }
}