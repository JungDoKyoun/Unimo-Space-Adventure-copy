using UnityEngine;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Muzzle")]

    public sealed class Muzzle : MonoBehaviour
    {
        public Projectile Launch(string projectileName)
        {
            var projectile = ProjectilePoolManager.Instance.Cloning(projectileName);

            projectile.transform.SetPositionAndRotation(transform);

            projectile.SetActive(true);

            return projectile;
        }
    }
}