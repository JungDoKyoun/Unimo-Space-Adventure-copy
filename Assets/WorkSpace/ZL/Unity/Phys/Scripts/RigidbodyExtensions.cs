using UnityEngine;

namespace ZL.Unity.Phys
{
    public static partial class RigidbodyExtensions
    {
        public static void MoveTowards(this Rigidbody instance, Vector3 target, float maxDistanceDelta)
        {
            maxDistanceDelta *= Time.fixedDeltaTime;

            var nextPosition = Vector3.MoveTowards(instance.position, target, maxDistanceDelta);

            instance.MovePosition(nextPosition);
        }

        public static void LookTowards(this Rigidbody instance, Vector3 target, float maxDegreesDelta)
        {
            var direction = (target - instance.position).normalized;

            if (direction.sqrMagnitude == 0f)
            {
                return;
            }

            var targetRotation = Quaternion.LookRotation(direction);

            maxDegreesDelta *= Time.fixedDeltaTime;

            var nextRotation = Quaternion.RotateTowards(instance.rotation, targetRotation, maxDegreesDelta);

            instance.MoveRotation(nextRotation);
        }
    }
}