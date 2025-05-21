using UnityEngine;

using ZL.Unity.Pooling;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Enemy Pool Manager")]

    public sealed class EnemyPoolManager : ObjectPoolManager<ProjectilePoolManager, Enemy>
    {

    }
}