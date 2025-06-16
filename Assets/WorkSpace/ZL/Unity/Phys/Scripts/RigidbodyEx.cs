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

        public static void LookTowards(this Rigidbody instance, Vector3 worldPosition, Axis freezeRotation, float maxDegreesDelta)
        {
            var to = instance.LookRotation(worldPosition, freezeRotation);

            maxDegreesDelta *= Time.fixedDeltaTime;

            var nextRotation = Quaternion.RotateTowards(instance.rotation, to, maxDegreesDelta);

            instance.MoveRotation(nextRotation);
        }

        public static Quaternion LookRotation(this Rigidbody instance, Vector3 worldPosition, Axis freezeRotation)
        {
            return LookRotation(instance, worldPosition, Vector3.up, freezeRotation);
        }

        public static Quaternion LookRotation(this Rigidbody instance, Vector3 worldPosition, Vector3 upwards, Axis freezeRotation)
        {
            var forward = (worldPosition - instance.position).normalized;

            return QuaternionEx.LookRotation(forward, upwards, freezeRotation);
        }
    }
}