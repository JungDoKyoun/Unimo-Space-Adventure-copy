using UnityEngine;

namespace ZL.Unity.GFX
{
    [AddComponentMenu("ZL/Graphics/Material Property Controller")]

    public sealed class MaterialPropertyController : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private MaterialController materialController = null;

        [Space]

        [SerializeField]

        private string propertyName = "";

        public void SetInt(int value)
        {
            materialController.Material.SetInt(propertyName, value);
        }

        public void SetFloat(float value)
        {
            materialController.Material.SetFloat(propertyName, value);
        }

        public void SetColor(Color value)
        {
            materialController.Material.SetColor(propertyName, value);
        }
    }
}