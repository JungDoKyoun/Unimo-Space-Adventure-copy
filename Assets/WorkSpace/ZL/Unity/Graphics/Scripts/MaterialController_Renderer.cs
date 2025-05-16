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

        #pragma warning disable CS0108

        private Renderer renderer = null;

        #pragma warning restore CS0108

        public override Material[] Materials
        {
            get => renderer.materials;
        }
    }
}