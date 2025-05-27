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

        private Graphic targetGraphic = null;

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
                targetGraphic.material = new Material(targetGraphic.material);
            }

            materials = new Material[1]
            {
                targetGraphic.material
            };
        }
    }
}