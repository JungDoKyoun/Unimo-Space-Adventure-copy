using JDG;

using System;

using System.Collections;

using UnityEngine;

using ZL.Unity.Singleton;

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

                Instance.OnMaxFuelChangedAction?.Invoke(maxFuel);

                Fuel = fuel;
            }
        }

        

        private static float fuel = 0f;

        public static float Fuel
        {
            get => fuel;

            set
            {
                fuel = Mathf.Clamp(value, 0f, maxFuel);

                Instance.OnFuelChangedAction?.Invoke(fuel);

                if (fuel == 0f)
                {
                    Instance.OnFuelEmpty?.Invoke();
                }
            }
        }

        public Action OnFuelEmpty = null;

        private StageData stageData = null;

        public event Action<float> OnMaxFuelChangedAction = null;

        public event Action<float> OnFuelChangedAction = null;

        private void Start()
        {
            stageData = StageData.Instance;

            if (GameStateManager.IsClear == true)
            {
                return;
            }

            fuel = 100f;

            MaxFuel = 100f;

            FixedDebug.Log("연료 초기화됨");
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
            while (true)
            {
                yield return null;

                Fuel -= stageData.FuelConsumptionAmount * Time.deltaTime;
            }
        }
    }
}