using System.Collections;

using UnityEngine;

using UnityEngine.Events;

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

        private SliderValueDisplayer fuelBar = null;

        [Space]

        [SerializeField]

        private float fuelMax = 0f;

        [SerializeField]

        private float fuelConsumptionAmount = 0f;

        [Space]

        [SerializeField]

        private UnityEvent onFuelEmptyEvent = null;

        private float fuel = 0f;

        public float Fuel
        {
            get => fuel;

            set
            {
                fuel = Mathf.Clamp(value, 0f, fuelMax);

                fuelBar.Slider.value = fuel;

                if (fuel == 0f)
                {
                    onFuelEmptyEvent.Invoke();
                }
            }
        }

        protected override void Awake()
        {
            base.Awake();

            fuelBar.Slider.maxValue = fuelMax;

            Fuel = fuelMax;
        }

        public void StartConsumption()
        {
            if (consumptionRoutine != null)
            {
                return;
            }

            consumptionRoutine = ConsumptionRoutine();

            StartCoroutine(consumptionRoutine);
        }

        public void StopConsumption()
        {
            if (consumptionRoutine == null)
            {
                return;
            }

            StopCoroutine(consumptionRoutine);

            consumptionRoutine = null;
        }

        private IEnumerator consumptionRoutine = null;

        private IEnumerator ConsumptionRoutine()
        {
            while (true)
            {
                yield return null;

                Fuel -= fuelConsumptionAmount * Time.deltaTime;
            }
        }
    }
}