using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZL.Unity.Unimo;

namespace JDG
{
    public class ShopUI : MonoBehaviour
    {
        [Header("UIâ �����Ҷ� �ʿ��� �͵�")]
        [SerializeField] private GameObject _root;
        [SerializeField] private Transform _slotParent;
        [SerializeField] private TextMeshProUGUI _resourceText1;
        [SerializeField] private TextMeshProUGUI _resourceText2;
        [SerializeField] private Sprite _resourceSpite;
        [SerializeField] private Image _resourceIcon1;
        [SerializeField] private Image _resourceIcon2;
        //Ȥ�� �ʿ��ұ�� ������ �ʿ������ �����
        //[SerializeField] private Image _repairIcon;
        //[SerializeField] private TextMeshProUGUI _repairButtonText1;
        //[SerializeField] private TextMeshProUGUI _repairButtonText2;
        private GameObject _slotPrefab;

        [Header("UIâ ��ġ ����")]
        [SerializeField] private Vector3 _offset;

        [Header("������ ������ ���� ����")]
        [SerializeField] private int _itemCount;

        [Header("������ ����")]
        [SerializeField] private int _repair1Price;
        [SerializeField] private int _repair2Price;

        private void Start()
        {
            HideShopUI();
        }

        public int ItemCount => _itemCount;

        public void OpenShopUI(List<RelicData> relics, Vector3 worldPos)
        {
            _root.SetActive(true);
            transform.position = worldPos + _offset;
            _slotPrefab = Resources.Load<GameObject>("WorldMap/RelicSlot");
            _resourceText1.text = _repair1Price.ToString();
            _resourceIcon1.sprite = _resourceSpite;
            _resourceText2.text = _repair2Price.ToString();
            _resourceIcon2.sprite = _resourceSpite;
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
            //������ ����
            if(ConditionChecker.IsEnoughPlayerResource(_repair1Price, ResourcesType.IngameCurrency))
            {
                FirebaseDataBaseMgr.Instance.UpdateRewardIngameCurrency(-_repair1Price);

                float maxHP = PlayerManager.PlayerStatus.maxHP;

                float tenPer = maxHP / 10;

                float currentHP = PlayerManager.PlayerStatus.currentHealth;
                currentHP += tenPer;
                if(currentHP >= maxHP)
                {
                    currentHP = maxHP;
                }
            }
        }

        public void On100RepairButtonClicked()
        {
            if (ConditionChecker.IsEnoughPlayerResource(_repair2Price, ResourcesType.IngameCurrency))
            {
                FirebaseDataBaseMgr.Instance.UpdateRewardIngameCurrency(-_repair2Price);

                float maxHP = PlayerManager.PlayerStatus.maxHP ;

                float currentHP = PlayerManager.PlayerStatus.currentHealth;
                currentHP += maxHP;
                if(currentHP >= maxHP)
                {
                    currentHP = maxHP;
                }
            }
        }
    }
}
