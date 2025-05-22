using UnityEngine;

namespace ZL.Unity.Unimo
{
    public abstract class EnemyTargetDitection : ScriptableObject
    {
        public abstract Transform FindTarget();
    }
}