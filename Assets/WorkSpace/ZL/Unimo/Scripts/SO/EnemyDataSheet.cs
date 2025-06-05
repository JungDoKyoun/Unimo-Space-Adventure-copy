using UnityEngine;

using ZL.Unity.IO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/Enemy Data Sheet (Singleton)", fileName = "Enemy Data Sheet")]

    public sealed class EnemyDataSheet : ScriptableGoogleSheet<EnemyDataSheet, EnemyData>
    {

    }
}