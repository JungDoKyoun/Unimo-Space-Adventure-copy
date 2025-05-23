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
        [SerializeField] private Vector3 _offset;

        private void Start()
        {
            HideShopUI();
        }

        public void OpenShopUI(List<RelicDataSO> relics, Vector3 worldPos)
        {
            _root.SetActive(true);
            transform.position = worldPos + _offset;

            foreach(Transform child in _slotParent)
            {
                Destroy(child.gameObject);
            }

            foreach(var relic in relics)
            {

            }
        }

        public void HideShopUI()
        {
            _root.SetActive(false);
        }
    }
}
