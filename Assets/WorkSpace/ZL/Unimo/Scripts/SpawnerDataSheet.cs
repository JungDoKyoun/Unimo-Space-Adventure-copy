using UnityEngine;

using ZL.Unity.IO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/Spawner Data Sheet", fileName = "Spawner Data Sheet")]

    public sealed class SpawnerDataSheet : ScriptableGoogleSheet<SpawnerData>
    {

    }
}