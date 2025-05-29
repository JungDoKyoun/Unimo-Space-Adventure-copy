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
        [SerializeField] private TextMeshProUGUI _resourceText1;
        [SerializeField] private TextMeshProUGUI _resourceText2;
        //혹시 필요할까봐 만들어둠 필요없으면 지울것
        //[SerializeField] private Image _repairIcon;
        //[SerializeField] private TextMeshProUGUI _repairButtonText1;
        //[SerializeField] private TextMeshProUGUI _repairButtonText2;
        //[SerializeField] private Image _resourceIcon1;
        //[SerializeField] private Image _resourceIcon2;
        private GameObject _slotPrefab;

        [Header("UI창 위치 조정")]
        [SerializeField] private Vector3 _offset;

        [Header("아이템 선택지 갯수 조정")]
        [SerializeField] private int _itemCount;

        [Header("수리비 조정")]
        [SerializeField] private int _repair1Price;
        [SerializeField] private int _repair2Price;

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
            _resourceText1.text = _repair1Price.ToString();
            _resourceText2.text = _repair2Price.ToString();
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
            //소지금 감소
            if(FirebaseDataBaseMgr.IngameCurrency >= _repair1Price)
            {
                FirebaseDataBaseMgr.Instance.UpdateRewardIngameCurrency(-_repair1Price);

                //체력 회복 코드 넣기
                int maxHP = 100; //맥스 HP받아오면 그걸로 고치기
                int tenPer = maxHP / 10;

                //인트 a 지우고 현재 체력 넣으면됨
                int a = 0;
                a += tenPer;
                if(a >= maxHP)
                {
                    a = maxHP;
                }
                Debug.Log(a);
            }

            Debug.Log("체력 10% 회복");
        }

        public void On100RepairButtonClicked()
        {
            //소지금 감소
            if (FirebaseDataBaseMgr.IngameCurrency >= _repair1Price)
            {
                FirebaseDataBaseMgr.Instance.UpdateRewardIngameCurrency(-_repair2Price);

                //체력 회복 코드 넣기
                int maxHP = 100; //맥스 HP받아오면 그걸로 고치기

                //인트 a 지우고 현재 체력 넣으면됨
                int a = 0;
                a += maxHP;
                if(a >= maxHP)
                {
                    a = maxHP;
                }
                Debug.Log(a);
            }
            Debug.Log("체력 100% 회복");
        }
    }
}
