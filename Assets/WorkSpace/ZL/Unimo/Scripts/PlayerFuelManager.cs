using JDG;

using System.Collections;

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

        private SliderValueDisplayer playerFuelBar = null;

        private static float fuelMax = 0f;

        public static float FuelMax
        {
            get => fuelMax;

            set
            {
                fuelMax = value;

                if (Instance.playerFuelBar != null)
                {
                    Instance.playerFuelBar.Slider.maxValue = fuelMax;
                }

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

                if (Instance.playerFuelBar != null)
                {
                    Instance.playerFuelBar.Slider.value = fuel;
                }

                if (fuel == 0f)
                {
                    StageSceneDirector.Instance.StageFail();
                }
            }
        }

        private void Start()
        {
            if (GameStateManager.IsClear == true)
            {
                return;
            }

            fuel = 100f;

            FuelMax = 100f;
        }

        public void StartConsumFuel()
        {
            if (consumFuelRoutine != null)
            {
                return;
            }

            consumFuelRoutine = ConsumFuelRoutine();

            StartCoroutine(consumFuelRoutine);
        }

        public void StopConsumFuel()
        {
            if (consumFuelRoutine == null)
            {
                return;
            }

            StopCoroutine(consumFuelRoutine);

            consumFuelRoutine = null;
        }

        private IEnumerator consumFuelRoutine = null;

        private IEnumerator ConsumFuelRoutine()
        {
            var fuelConsumption = StageData.Instance.FuelConsumptionAmount;

            while (true)
            {
                yield return null;

                Fuel -= fuelConsumption * Time.deltaTime;
            }
        }
    }
}