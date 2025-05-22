using UnityEngine;

using ZL.Unity.IO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/Enemy Data Sheet", fileName = "Enemy Data Sheet")]

    public sealed class EnemyDataSheet : ScriptableSheet<EnemyData>
    {

    }
}