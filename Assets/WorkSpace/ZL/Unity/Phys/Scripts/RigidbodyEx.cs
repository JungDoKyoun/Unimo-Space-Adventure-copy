using System;

using UnityEngine;

using UnityEngine.Animations;

using ZL.CS;

namespace ZL.Unity.Phys
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

        public static void LookTowards(this Rigidbody instance, Transform target, Axis freezeRotation, float maxDegreesDelta)
        {
            LookTowards(instance, target.position, freezeRotation, maxDegreesDelta);
        }

        public static void LookTowards(this Rigidbody instance, Vector3 targetPosition, Axis freezeRotation, float maxDegreesDelta)
        {
            var lookRotation = instance.LookRotation(targetPosition, freezeRotation);

            maxDegreesDelta *= Time.fixedDeltaTime;

            var nextRotation = Quaternion.RotateTowards(instance.rotation, lookRotation, maxDegreesDelta);

            instance.MoveRotation(nextRotation);
        }

        public static Quaternion LookRotation(this Rigidbody instance, Transform target, Axis freezeRotation)
        {
            return LookRotation(instance, target.position, freezeRotation);
        }

        public static Quaternion LookRotation(this Rigidbody instance, Vector3 targetPosition, Axis freezeRotation)
        {
            var forward = (targetPosition - instance.position).normalized;

            return QuaternionEx.LookRotation(forward, freezeRotation);
        }
    }

    public static partial class QuaternionEx
    {
        public static Quaternion LookRotation(Vector3 forward, Axis freezeRotation)
        {
            if (freezeRotation.Contains(Axis.X) == true)
            {
                forward.y = 0f;
            }

            if (freezeRotation.Contains(Axis.Y) == true)
            {
                forward.x = 0f;
            }

            if (freezeRotation.Contains(Axis.Z) == true)
            {
                forward.z = 0f;
            }

            if (forward.sqrMagnitude == 0f)
            {
                return Quaternion.identity;
            }

            return Quaternion.LookRotation(forward);
        }
    }
}