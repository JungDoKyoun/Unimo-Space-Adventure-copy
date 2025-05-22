using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JDG;
using TMPro;
using UnityEngine.UI;
using System;

namespace JDG
{
    public class ShopItemSlot : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _relicName;
        [SerializeField] private Image _relicIcon;
        [SerializeField] private Image _resourceIcon;
        [SerializeField] private TextMeshProUGUI _relicPrice;

        public void SetShopItemSlot(string name, Sprite relicIcon, Sprite resourceIcon, string price)
        {
            _relicName.text = name;
            _relicIcon.sprite = relicIcon;
            _resourceIcon.sprite = resourceIcon;
            _relicPrice.text = price;
        }
    }
}
