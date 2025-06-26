using UnityEngine;

using ZL.Unity.UI;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Player Energy Bar")]

    public sealed class PlayerEnergyBar : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        private SliderValueDisplayer sliderValueDisplayer = null;

        private void Awake()
        {
            sliderValueDisplayer.SetMaxValue(10);

            sliderValueDisplayer.SetValue(0);

            PlayerManager.OnEnergyChanged += sliderValueDisplayer.SetValue;
        }

        private void OnDestroy()
        {
            PlayerManager.OnEnergyChanged -= sliderValueDisplayer.SetValue;
        }
    }
}