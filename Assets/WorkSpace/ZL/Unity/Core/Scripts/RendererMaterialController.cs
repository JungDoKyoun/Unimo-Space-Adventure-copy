using UnityEngine;

namespace ZL.Unity
{
    [AddComponentMenu("ZL/Renderer Material Controller")]

    public sealed class RendererMaterialController : MaterialController
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [ReadOnly(true)]

        private new Renderer renderer;

        public override Material[] Materials
        {
            get => renderer.materials;
        }
    }
}