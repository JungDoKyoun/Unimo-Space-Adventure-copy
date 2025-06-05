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
        [SerializeField] private Button _buyButton;
        [SerializeField] private GameObject _disabledOverlay;
        private RelicDataSO _relicData;
        private bool _isBuy;

        public void SetShopItemSlot(RelicDataSO data)
        {
            _relicData = data;
            _relicName.text = data._relicName;
            _relicIcon.sprite = data._relicImage;
            _resourceIcon.sprite = data._relicPrice._resourceData._resourcesIcon;
            _relicPrice.text = data._relicPrice._value.ToString();

            _isBuy = false;
            _disabledOverlay.SetActive(false);
            _buyButton.interactable = true;
        }

        public void OnBuyButtonClicked()
        {
            ResourcesType resourcesType = _relicData._relicPrice._resourceData._resourcesType; //������ �ڿ� Ÿ��
            int relicPrice = _relicData._relicPrice._value; //������ ����

            //�÷��̾� ������ ����
            if(ConditionChecker.IsEnoughPlayerResource(relicPrice, resourcesType))
            {
                FirebaseDataBaseMgr.Instance.UpdateRewardIngameCurrency(-relicPrice);

                //���� �ۼ��Ǹ� �ش� ���� ȿ���� �ɷ�ġ ���

                _disabledOverlay.SetActive(true);
                _buyButton.interactable = false;
            }
            else
            {
                Debug.Log("������ ����");
            }
        }
    }
}
