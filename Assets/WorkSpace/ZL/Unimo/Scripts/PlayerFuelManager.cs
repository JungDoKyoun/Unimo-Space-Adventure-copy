using UnityEngine;

using ZL.Unity.Singleton;

using ZL.Unity.UI;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Player Fuel Manager (Singleton)")]

    public sealed class PlayerFuelManager : MonoSingleton<PlayerFuelManager>
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        private SliderValueDisplayer PlayerFuelBar = null;

        private static float fuelMax = 0f;

        public static float FuelMax
        {
            get => fuelMax;

            set
            {
                fuelMax = value;

                Fuel = fuel;
            }
        }

        private static float fuel = 0f;

        public static float Fuel
        {
            get => fuel;

            set
            {
                fuel = Mathf.Clamp(value, 0f, fuelMax);

                Instance.PlayerFuelBar.Slider.value = fuel;

                if (fuel == 0f)
                {
                    StageDataManager.Instance.StageFail();
                }
            }
        }

        public void Initialize()
        {
            PlayerFuelBar.Slider.maxValue = 100f;

            Fuel = fuelMax;
        }
    }
}