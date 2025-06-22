using UnityEngine;

using UnityEngine.Animations;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Boss Monster 1 (Spawned")]

    public sealed class BossMonster1 : Enemy
    {
        private void FixedUpdate()
        {
            if (isStoped == true)
            {
                return;
            }

            if (rotationSpeed != 0f)
            {
                rigidbody.LookTowards(Destination.position, rotationSpeed * Time.fixedDeltaTime, Axis.Y);
            }

            if (enemyData.MovementSpeed != 0f)
            {
                rigidbody.MoveForward(enemyData.MovementSpeed * Time.fixedDeltaTime);
            }
        }
    }
}