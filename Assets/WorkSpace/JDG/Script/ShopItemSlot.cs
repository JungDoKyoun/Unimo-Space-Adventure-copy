using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JDG;
using TMPro;
using UnityEngine.UI;
using System;
using ZL.Unity.Unimo;
using ZL.Unity;

namespace JDG
{
    public class ShopItemSlot : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _relicName;
        [SerializeField] private Image _relicIcon;
        [SerializeField] private Image _resourceIcon;
        [SerializeField] private TextMeshProUGUI _relicPrice;
        [SerializeField] private Button _buyButton;
        [SerializeField] private GameObject _disabledOverlay;
        [SerializeField] private ImageTable _imageTable;
        [SerializeField] private Sprite _resourceSpite;
        private RelicData _relicData;
        private bool _isBuy;

        public void SetShopItemSlot(RelicData data)
        {
            _relicData = data;
            _relicName.text = data.name;

            if (_imageTable != null && _imageTable[data.name] != null)
                _relicIcon.sprite = _imageTable[data.name];

            if(_resourceSpite != null)
                _resourceIcon.sprite = _resourceSpite;

            _relicPrice.text = data.Price.ToString();

            _isBuy = false;
            _disabledOverlay.SetActive(false);
            _buyButton.interactable = true;
        }

        public void OnBuyButtonClicked()
        {
            int relicPrice = _relicData.Price; //아이템 가격

            //플레이어 소지금 감소
            if(ConditionChecker.IsEnoughPlayerResource(relicPrice, ResourcesType.IngameCurrency))
            {
                FirebaseDataBaseMgr.Instance.UpdateRewardIngameCurrency(-relicPrice);

                PlayerInventoryManager.AddRelic(_relicData);

                _disabledOverlay.SetActive(true);
                _buyButton.interactable = false;
            }
            else
            {
                Debug.Log("소지금 부족");
            }
        }
    }
}
