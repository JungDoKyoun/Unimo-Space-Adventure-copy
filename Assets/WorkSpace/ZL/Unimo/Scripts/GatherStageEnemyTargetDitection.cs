using UnityEngine;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/Gather Stage Enemy Target Ditection", fileName = "Gather Stage Enemy Target Ditection")]

    public sealed class GatherStageEnemyTargetDitection : EnemyTargetDitection
    {
        public override Transform FindTarget()
        {
            return GatherStageSceneDirector.Instance.EnemyTarget;
        }
    }
}