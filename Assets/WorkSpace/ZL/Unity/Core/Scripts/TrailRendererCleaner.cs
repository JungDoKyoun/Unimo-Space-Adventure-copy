using UnityEngine;

namespace ZL.Unity
{
    [AddComponentMenu("ZL/Trail Renderer Cleaner")]

    public sealed class TrailRendererCleaner : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        private TrailRenderer trailRenderer = null;

        private void OnDisable()
        {
            trailRenderer.Clear();
        }
    }
}