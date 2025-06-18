using UnityEngine;

using ZL.Unity.UI;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Player Fuel Bar")]

    public sealed class PlayerFuelBar : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [ReadOnlyWhenPlayMode]

        private SliderValueDisplayer sliderValueDisplayer = null;

        private void Start()
        {
            sliderValueDisplayer.SetMaxValue(PlayerFuelManager.MaxFuel);

            sliderValueDisplayer.SetValue(PlayerFuelManager.Fuel);

            PlayerFuelManager.Instance.OnMaxFuelChanged += sliderValueDisplayer.SetMaxValue;

            PlayerFuelManager.Instance.OnFuelChanged += sliderValueDisplayer.SetValue;
        }
    }
}