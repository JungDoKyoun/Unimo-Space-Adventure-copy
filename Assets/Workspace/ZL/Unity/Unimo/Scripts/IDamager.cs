using UnityEngine;

namespace ZL.Unity.Unimo
{
    public interface IDamager
    {
        public void GiveDamage(IDamageable damageable, Vector3 contact);
    }
}