using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace JDG
{
    public class ShopUI : MonoBehaviour
    {
        [Header("UIâ �����Ҷ� �ʿ��� �͵�")]
        [SerializeField] private GameObject _root;
        [SerializeField] private Transform _slotParent;
        private GameObject _slotPrefab;
        [SerializeField] private Image _repairIcon;
        [SerializeField] private TextMeshProUGUI _repairButtonText1;
        [SerializeField] private TextMeshProUGUI _repairButtonText2;
        [SerializeField] private Image _resourceIcon1;
        [SerializeField] private Image _resourceIcon2;
        [SerializeField] private TextMeshProUGUI _resourceText1;
        [SerializeField] private TextMeshProUGUI _resourceText2;
        private Action _onShopClosed;

        [Header("UIâ ��ġ ����")]
        [SerializeField] private Vector3 _offset;

        private void Start()
        {
            HideShopUI();
        }

        public Action OnShopClosed { get { return _onShopClosed; } set { _onShopClosed = value; } }

        public void OpenShopUI(List<RelicDataSO> relics, Vector3 worldPos)
        {
            _root.SetActive(true);
            transform.position = worldPos + _offset;
            _slotPrefab = Resources.Load<GameObject>("WorldMap/RelicSlot");
            Debug.Log(relics.Count);
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
            Debug.Log("�ݱ� ��ư ����");
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
