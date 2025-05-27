using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JDG
{
    public class ShopUI : MonoBehaviour
    {
        [Header("UI창 생성할때 필요한 것들")]
        [SerializeField] private GameObject _root;
        [SerializeField] private Transform _slotParent;
        //혹시 필요할까봐 만들어둠 필요없으면 지울것
        //[SerializeField] private Image _repairIcon;
        //[SerializeField] private TextMeshProUGUI _repairButtonText1;
        //[SerializeField] private TextMeshProUGUI _repairButtonText2;
        //[SerializeField] private Image _resourceIcon1;
        //[SerializeField] private Image _resourceIcon2;
        //[SerializeField] private TextMeshProUGUI _resourceText1;
        //[SerializeField] private TextMeshProUGUI _resourceText2;
        private GameObject _slotPrefab;

        [Header("UI창 위치 조정")]
        [SerializeField] private Vector3 _offset;

        [Header("아이템 선택지 갯수 조정")]
        [SerializeField] private int _itemCount;

        private void Start()
        {
            HideShopUI();
        }

        public int ItemCount => _itemCount;

        public void OpenShopUI(List<RelicDataSO> relics, Vector3 worldPos)
        {
            _root.SetActive(true);
            transform.position = worldPos + _offset;
            _slotPrefab = Resources.Load<GameObject>("WorldMap/RelicSlot");
            foreach (Transform child in _slotParent)
            {
                Destroy(child.gameObject);
            }

            foreach(var relic in relics)
            {
                GameObject obj = Instantiate(_slotPrefab, _slotParent);
                ShopItemSlot shopSlot = obj.GetComponent<ShopItemSlot>();
                shopSlot.SetShopItemSlot(relic);
            }
        }

        public void HideShopUI()
        {
            _root.SetActive(false);
            UIManager.Instance.IsUIOpen = false;
        }

        public void On10RepairButtonClicked()
        {
            //체력 회복 및 보유 자원 감소
            Debug.Log("체력 10% 회복");
        }

        public void On100RepairButtonClicked()
        {
            //체력 회복 및 보유 자원 감소
            Debug.Log("체력 100% 회복");
        }
    }
}
