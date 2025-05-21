using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JDG
{
    public class TopBarUI : MonoBehaviour
    {
        [SerializeField] private Image _playerImageUI;
        [SerializeField] private Image _hpBarUI;
        [SerializeField] private Image _timeBarUI;
        [SerializeField] private Sprite _playerImage;
        [SerializeField] private int _maxHP;
        [SerializeField] private int _curHP;
        [SerializeField] private int _timeLimitMax;
        [SerializeField] private int _remainingTime;

        public void Init(int maxHP, int timeLimitMax)
        {
            _maxHP = maxHP;
            _remainingTime = timeLimitMax;

            if (_playerImage != null)
                _playerImageUI.sprite = _playerImage;

            _curHP = _maxHP;
            _remainingTime = _timeLimitMax;

            UpdateUI(_curHP, _remainingTime);

        }

        public void UpdateUI(int HP, int Time)
        {
            _curHP = HP;
            _remainingTime = Time;

            if (_hpBarUI != null)
                _hpBarUI.fillAmount = Mathf.Clamp01((float)_curHP / _maxHP);

            if (_timeBarUI != null)
                _timeBarUI.fillAmount = Mathf.Clamp01((float)_remainingTime / _timeLimitMax);
        }
    }
}
