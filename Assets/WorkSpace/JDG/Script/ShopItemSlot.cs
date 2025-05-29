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
            ResourcesType resourcesType = _relicData._relicPrice._resourceData._resourcesType; //아이템 자원 타입
            int relicPrice = _relicData._relicPrice._value; //아이템 가격
            Debug.Log("구매 완료");

            //플레이어 소지금 감소
            if(FirebaseDataBaseMgr.IngameCurrency >= relicPrice)
            {
                FirebaseDataBaseMgr.Instance.UpdateRewardIngameCurrency(-relicPrice);

                //유물 작성되면 해당 유물 효과로 능력치 상승
            }

            //구매에 성공했을때 구매버튼 비활성화 위에서 구매 성공시 아래 코드 마지막 구매성공 코드에 넣고 아래 코드 지우기
            _disabledOverlay.SetActive(true);
            _buyButton.interactable = false;
        }
    }
}
