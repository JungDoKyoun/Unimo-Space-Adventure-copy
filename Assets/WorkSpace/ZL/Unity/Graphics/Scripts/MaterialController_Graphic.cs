using UnityEngine;

using UnityEngine.UI;

namespace ZL.Unity.GFX
{
    [AddComponentMenu("ZL/GFX/Material Controller (Graphic)")]

    public sealed class MaterialController_Graphic : MaterialController
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private Graphic graphic = null;

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