using UnityEngine;

namespace ZL.Unity.GFX
{
    [AddComponentMenu("ZL/GFX/Material Controller (Renderer)")]

    public sealed class MaterialController_Renderer : MaterialController
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private Renderer targetRenderer = null;

        public override Material[] Materials
        {
            get => targetRenderer.materials;
        }
    }
}