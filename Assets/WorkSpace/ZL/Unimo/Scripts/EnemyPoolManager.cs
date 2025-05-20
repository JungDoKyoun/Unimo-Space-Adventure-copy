using UnityEngine;

using ZL.Unity.Pooling;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Enemy Pool Manager")]

    public class EnemyPoolManager : ObjectPoolManager<EnemyPoolManager, Enemy>
    {

    }
}