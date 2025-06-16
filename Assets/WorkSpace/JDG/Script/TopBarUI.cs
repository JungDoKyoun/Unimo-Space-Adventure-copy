using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JDG;
using ZL.Unity.Unimo;
using TMPro;

namespace JDG
{
    public class TopBarUI : MonoBehaviour
    {
        [SerializeField] private Image _playerImageUI;
        [SerializeField] private Image _hpBarUI;
        [SerializeField] private Image _timeBarUI;
        [SerializeField] private Sprite _playerImage;
        [SerializeField] private TextMeshProUGUI _playerHPText;
        [SerializeField] private TextMeshProUGUI _playerFuelText;
        [SerializeField] private float _maxHP;
        [SerializeField] private float _curHP;
        [SerializeField] private float _timeLimitMax;
        [SerializeField] private float _remainingTime;

        private void Start()
        {
            _maxHP = PlayerManager.PlayerStatus.maxHP;
            _timeLimitMax = PlayerFuelManager.FuelMax;

            if (_playerImage != null)
                _playerImageUI.sprite = _playerImage;

            _curHP = PlayerManager.PlayerStatus.currentHealth;
            _remainingTime = PlayerFuelManager.Fuel;

            UpdateUI(_curHP, _remainingTime);
        }

        private void OnEnable()
        {
            PlayerEvents.OnHPChanged += UpdateHPBar;
            PlayerEvents.OnFuelChanged += UpdateFuelBar;
        }

        private void OnDisable()
        {
            PlayerEvents.OnHPChanged -= UpdateHPBar;
            PlayerEvents.OnFuelChanged -= UpdateFuelBar;
        }

        private void UpdateHPBar(float maxHP, float currentHP)
        {
            _maxHP = maxHP;
            _curHP = currentHP;
            _hpBarUI.fillAmount = Mathf.Clamp01((float)_curHP / _maxHP);
            _playerHPText.text = $"{_curHP} / {_maxHP}";
        }

        private void UpdateFuelBar(float maxFuel, float currentFule)
        {
            _timeLimitMax = maxFuel;
            _remainingTime = currentFule;
            _timeBarUI.fillAmount = Mathf.Clamp01((float)_remainingTime / _timeLimitMax);
            _playerFuelText.text = $"{_remainingTime} / {_timeLimitMax}";
        }

        public void UpdateUI(float HP, float Time)
        {
            _curHP = HP;
            _remainingTime = Time;

            if (_hpBarUI != null)
            {
                _hpBarUI.fillAmount = Mathf.Clamp01((float)_curHP / _maxHP);
                _playerHPText.text = $"{_curHP} / {_maxHP}";

            }

            if (_timeBarUI != null)
            {
                _timeBarUI.fillAmount = Mathf.Clamp01((float)_remainingTime / _timeLimitMax);
                _playerFuelText.text = $"{_remainingTime} / {_timeLimitMax}";
            }
        }
    }
}
