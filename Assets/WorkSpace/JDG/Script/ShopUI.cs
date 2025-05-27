using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JDG
{
    public class ShopUI : MonoBehaviour
    {
        [Header("UIâ �����Ҷ� �ʿ��� �͵�")]
        [SerializeField] private GameObject _root;
        [SerializeField] private Transform _slotParent;
        //Ȥ�� �ʿ��ұ�� ������ �ʿ������ �����
        //[SerializeField] private Image _repairIcon;
        //[SerializeField] private TextMeshProUGUI _repairButtonText1;
        //[SerializeField] private TextMeshProUGUI _repairButtonText2;
        //[SerializeField] private Image _resourceIcon1;
        //[SerializeField] private Image _resourceIcon2;
        //[SerializeField] private TextMeshProUGUI _resourceText1;
        //[SerializeField] private TextMeshProUGUI _resourceText2;
        private GameObject _slotPrefab;

        [Header("UIâ ��ġ ����")]
        [SerializeField] private Vector3 _offset;

        [Header("������ ������ ���� ����")]
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
            //ü�� ȸ�� �� ���� �ڿ� ����
            Debug.Log("ü�� 10% ȸ��");
        }

        public void On100RepairButtonClicked()
        {
            //ü�� ȸ�� �� ���� �ڿ� ����
            Debug.Log("ü�� 100% ȸ��");
        }
    }
}
