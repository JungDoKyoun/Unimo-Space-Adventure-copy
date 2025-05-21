using UnityEngine;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/Stage Enemy Target Ditection", fileName = "Stage Enemy Target Ditection")]

    public sealed class StageEnemyTargetDitection : EnemyTargetDitection
    {
        public override Transform FindTarget()
        {
            return StageSceneDirector.Instance.EnemyTarget;
        }
    }
}