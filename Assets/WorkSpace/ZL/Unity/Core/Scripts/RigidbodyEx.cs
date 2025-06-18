using UnityEngine;

using UnityEngine.Animations;

namespace ZL.Unity
{
    public static partial class RigidbodyEx
    {
        public static void MoveTowards(this Rigidbody instance, Transform target, float maxDistanceDelta)
        {
            MoveTowards(instance, target.position, maxDistanceDelta);
        }

        public static void MoveTowards(this Rigidbody instance, Vector3 targetPosition, float maxDistanceDelta)
        {
            maxDistanceDelta *= Time.fixedDeltaTime;

            var nextPosition = Vector3.MoveTowards(instance.position, targetPosition, maxDistanceDelta);

            instance.MovePosition(nextPosition);
        }

        public static void ForceMovePosition(this Rigidbody instance, Vector3 position)
        {
            var constraints = instance.constraints;

            instance.constraints = ~(RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ);

            instance.MovePosition(position);

            instance.constraints = constraints;
        }

        public static void LookTowards(this Rigidbody instance, Vector3 worldPosition, Axis ignoreAxes, float maxDegreesDelta)
        {
            LookTowards(instance, worldPosition, Vector3.up, ignoreAxes, maxDegreesDelta);
        }

        public static void LookTowards(this Rigidbody instance, Vector3 worldPosition, Vector3 upwards, Axis ignoreAxes, float maxDegreesDelta)
        {
            var targetRotation = QuaternionEx.LookRotation(instance.position, worldPosition, upwards, ignoreAxes);

            maxDegreesDelta *= Time.fixedDeltaTime;

            var nextRotation = Quaternion.RotateTowards(instance.rotation, targetRotation, maxDegreesDelta);

            instance.MoveRotation(nextRotation);
        }
    }
}