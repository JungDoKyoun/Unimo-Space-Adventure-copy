using UnityEngine;

using ZL.Unity.IO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/Spawner Data Sheet (Singleton)", fileName = "Spawner Data Sheet (Singleton)")]

    public sealed class SpawnerDataSheet : ScriptableGoogleSheet<SpawnerDataSheet, SpawnerData>
    {

    }
}