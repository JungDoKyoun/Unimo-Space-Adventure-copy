using UnityEngine;

using UnityEngine.UI;

namespace ZL.Unity.GFX
{
    [AddComponentMenu("ZL/GFX/Material Controller (Graphic)")]

    [DefaultExecutionOrder((int)ScriptExecutionOrder.Awake)]

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

        public override Material Material
        {
            get => targetGraphic.material;
        }

        private void Awake()
        {
            if (isShared == false)
            {
                targetGraphic.material = new Material(targetGraphic.material);
            }
        }
    }
}