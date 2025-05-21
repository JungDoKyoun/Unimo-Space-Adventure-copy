using UnityEngine;

using ZL.Unity.Pooling;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Projectile Pool Manager")]

    public sealed class ProjectilePoolManager : ObjectPoolManager<ProjectilePoolManager, Projectile>
    {

    }
}