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
        [Header("UI창 생성할때 필요한 것들")]
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
        public System.Action OnShopClosed;

        [Header("UI창 위치 조정")]
        [SerializeField] private Vector3 _offset;

        private void Start()
        {
            HideShopUI();
        }

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
                shopSlot.SetShopItemSlot(relic._relicName, relic._relicImage, relic._relicPrice._resourceicon, relic._relicPrice._value);
            }
        }

        public void HideShopUI()
        {
            _root.SetActive(false);
            OnShopClosed?.Invoke();
        }
    }
}
