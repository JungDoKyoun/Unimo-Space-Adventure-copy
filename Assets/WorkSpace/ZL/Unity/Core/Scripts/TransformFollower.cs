using UnityEngine;

namespace ZL.Unity
{
    [AddComponentMenu("ZL/Transform Follower")]

    public sealed class TransformFollower : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        private Transform targetTransform = null;

        [Space]

        [SerializeField]

        private Vector3 offset = Vector3.zero;

        [Space]

        [SerializeField]

        private bool syncRotation = false;

        private void LateUpdate()
        {
            transform.position = targetTransform.position + offset;

            if (syncRotation == true)
            {
                transform.rotation = targetTransform.rotation;
            }
        }
    }
}