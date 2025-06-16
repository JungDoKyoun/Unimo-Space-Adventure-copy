using UnityEngine;

using ZL.Unity.UI;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Player Health Bar")]

    public sealed class PlayerHealthBar : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [ReadOnlyWhenPlayMode]

        private SliderValueDisplayer sliderValueDisplayer = null;

        private void Start()
        {
            sliderValueDisplayer.SetMaxValue(PlayerManager.PlayerStatus.maxHealth);

            sliderValueDisplayer.SetValue(PlayerManager.PlayerStatus.currentHealth);

            PlayerManager.OnHealthChanged += sliderValueDisplayer.SetValue;
        }

        private void OnDestroy()
        {
            PlayerManager.OnHealthChanged -= sliderValueDisplayer.SetValue;
        }
    }
}