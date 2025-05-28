using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JDG;
using UnityEngine.UI;
using TMPro;

namespace JDG
{
    public class ScriptEventUI : MonoBehaviour
    {
        [Header("UIâ �����Ҷ� �ʿ��� �͵�")]
        [SerializeField] private GameObject _root;
        [SerializeField] private Transform _choiceSlotParent;
        [SerializeField] private Image _scriptEventImage;
        [SerializeField] private TextMeshProUGUI _scriptEventName;
        [SerializeField] private TextMeshProUGUI _scriptEventDesc;

        [Header("UIâ ��ġ ����")]
        [SerializeField] private Vector3 _offSet;

        [Header("������ ���� ����")]
        [SerializeField] private int _choiceCount;

        private GameObject _choiceShlotPrefab;

        private void Start()
        {
            _root.SetActive(false);
        }

        public int ChoiceCount => _choiceCount;

        public void OpenScriptEventUI(EventDataSO eventData, List<ChoiceDataSO> choiceDatas, Vector3 worldPos)
        {
            _root.SetActive(true);
            transform.position = worldPos + _offSet;
            _scriptEventImage.sprite = eventData._eventImage;
            _scriptEventName.text = eventData._eventTitle;
            _scriptEventDesc.text = eventData._eventDesc;
            _choiceShlotPrefab = Resources.Load<GameObject>("WorldMap/ScriptEventChoiceShlot");

            foreach(Transform child in _choiceSlotParent)
            {
                Destroy(child.gameObject);
            }

            foreach(ChoiceDataSO choiceData in choiceDatas)
            {
                GameObject obj = Instantiate(_choiceShlotPrefab, _choiceSlotParent);
                ScriptEventChoiceShlot slot = obj.GetComponent<ScriptEventChoiceShlot>();
                slot.SetScriptEventChoiceShlot(choiceData);
            }
        }

        public void HideUI()
        {
            _root.SetActive(false);
            UIManager.Instance.IsUIOpen = false;
        }
    }
}
