using UnityEngine;

using UnityEngine.UI;

namespace ZL.Unity
{
    [AddComponentMenu("ZL/Graphic Material Controller")]

    public sealed class GraphicMaterialController : MaterialController
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [ReadOnly(true)]

        private Graphic graphic;

        [Space]

        [SerializeField]

        private bool isShared = false;

        private Material[] materials;

        public override Material[] Materials
        {
            get => materials;
        }

        private void Awake()
        {
            if (isShared == false)
            {
                graphic.material = new Material(graphic.material);
            }

            materials = new Material[1]
            {
                graphic.material
            };
        }
    }
}