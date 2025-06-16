using JDG;
using System;
using System.Collections;

using UnityEngine;

using ZL.Unity.Singleton;

using ZL.Unity.UI;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Player Fuel Manager (Singleton)")]

    public sealed class PlayerFuelManager : MonoSingleton<PlayerFuelManager>
    {
        private static float maxFuel = 0f;

        public static float MaxFuel
        {
            get => maxFuel;

            set
            {
                maxFuel = value;

                OnMaxFuelChanged?.Invoke(maxFuel);

                Fuel = fuel;
            }
        }

        public static event Action<float> OnMaxFuelChanged = null;

        private static float fuel = 0f;

        public static float Fuel
        {
            get => fuel;

            set
            {
                fuel = Mathf.Clamp(value, 0f, maxFuel);

                OnFuelChanged?.Invoke(fuel);

                if (fuel == 0f)
                {
                    StageSceneDirector.Instance.StageFail();
                }
            }
        }

        public static event Action<float> OnFuelChanged = null;

        protected override void Awake()
        {
            base.Awake();

            if (GameStateManager.IsClear == true)
            {
                return;
            }

            fuel = 100f;

            MaxFuel = 100f;
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