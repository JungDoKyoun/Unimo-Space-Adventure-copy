using UnityEngine;

using UnityEngine.Animations;

namespace ZL.Unity
{
    public static partial class RigidbodyEx
    {
        public static void MoveForward(this Rigidbody instance, float distance)
        {
            var nextPosition = instance.position + instance.rotation * Vector3.forward * distance;

            instance.MovePosition(nextPosition);
        }

        public static void MoveTowards(this Rigidbody instance, Transform target, float maxDistanceDelta)
        {
            MoveTowards(instance, target.position, maxDistanceDelta);
        }

        public static void MoveTowards(this Rigidbody instance, Vector3 targetPosition, float maxDistanceDelta)
        {
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

        public static void LookTowards(this Rigidbody instance, Vector3 worldPosition, float maxDegreesDelta, Axis ignoreAxes)
        {
            LookTowards(instance, worldPosition, Vector3.up, maxDegreesDelta, ignoreAxes);
        }

        public static void LookTowards(this Rigidbody instance, Vector3 worldPosition, Vector3 upwards, float maxDegreesDelta, Axis ignoreAxes)
        {
            if (QuaternionEx.TryLookRotation(instance.position, worldPosition, upwards, ignoreAxes, out var targetRotation) == false)
            {
                return;
            }

            var nextRotation = Quaternion.RotateTowards(instance.rotation, targetRotation, maxDegreesDelta);

            instance.MoveRotation(nextRotation);
        }
    }
}