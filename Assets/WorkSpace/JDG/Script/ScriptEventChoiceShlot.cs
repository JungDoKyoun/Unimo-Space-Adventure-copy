using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace JDG
{
    public class ScriptEventChoiceShlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("���� ������ �ʿ��� �͵�")]
        [SerializeField] private RectTransform _nameTextTransform;
        [SerializeField] private RectTransform _descTextTransform;
        [SerializeField] private RectTransform _maskAreaTransform;
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _choiceName;
        [SerializeField] private TextMeshProUGUI _choiceDesc;
        [SerializeField] private Sprite _buttonUsefulImage;
        [SerializeField] private Sprite _buttonHarmfulImage;
        [SerializeField] private Sprite _buttonUseAndHarmfulImage;

        [Header("���� �ӵ� ����")]
        [SerializeField] private float _scrollSpeed;
        [SerializeField] private float _pauseTime;

        private ChoiceDataSO _choiceData;
        private Coroutine _nameScrollCo;
        private Coroutine _descScrollCo;
        private float _nameDefaultX;
        private float _descDefaultX;
        private float _nameScrollLength;
        private float _descScrollLength;

        private void Start()
        {
            _nameDefaultX = _nameTextTransform.anchoredPosition.x;
            var nameTmp = _nameTextTransform.GetComponent<TextMeshProUGUI>();
            float nameTextLength = nameTmp.preferredWidth;
            float maskAreaLength = _maskAreaTransform.rect.width;
            _nameScrollLength = Mathf.Max(0, nameTextLength - maskAreaLength);
            _descDefaultX = _descTextTransform.anchoredPosition.x;
            var descTmp = _descTextTransform.GetComponent<TextMeshProUGUI>();
            float descTextLength = descTmp.preferredWidth;
            _descScrollLength = Mathf.Max(0, descTextLength - maskAreaLength);
        }

        public void SetScriptEventChoiceShlot(ChoiceDataSO choiceData)
        {
            _choiceData = choiceData;

            //�������� �طο����� ���� ��ư ����ٲٴ� �ڵ� �ʿ������ �Ŀ� ����
            //bool isUseful = false;
            //bool isHarmful = false;

            //foreach (var probEffect in choiceData._probabilisticEffect)
            //{
            //    foreach (var effect in probEffect._effects)
            //    {
            //        if (effect._choiceEffectType == ChoiceEffectType.Useful)
            //        {
            //            isUseful = true;
            //        }
            //        else if (effect._choiceEffectType == ChoiceEffectType.Harmful)
            //        {
            //            isHarmful = true;
            //        }
            //        else if (effect._choiceEffectType == ChoiceEffectType.None)
            //        {
            //            isUseful = true;
            //        }

            //        if (isUseful && isHarmful)
            //            break;
            //    }

            //    if (isUseful && isHarmful)
            //        break;
            //}

            //if (isUseful && isHarmful)
            //{
            //    _button.image.sprite = _buttonUseAndHarmfulImage;
            //}
            //else if (isUseful)
            //{
            //    _button.image.sprite = _buttonUsefulImage;
            //}
            //else if (isHarmful)
            //{
            //    _button.image.sprite = _buttonHarmfulImage;
            //}

            //������ ȿ���� ������ �ʷϻ����� �طο� ȿ���� ������ ����������
            List<string> allDes = new List<string>();

            foreach (var prob in choiceData._probabilisticEffect)
            {
                List<string> midleDes = new List<string>();

                foreach (var effeect in prob._effects)
                {
                    string des = GetEffectText(effeect);

                    switch (effeect._choiceEffectType)
                    {
                        case ChoiceEffectType.Useful:
                            midleDes.Add($"<color=#22CD1C>{des}</color>");
                            break;
                        case ChoiceEffectType.Harmful:
                            midleDes.Add($"<color=red>{des}</color>");
                            break;
                        case ChoiceEffectType.None:
                            midleDes.Add(des);
                            break;
                    }
                }

                string temp = string.Join(",", midleDes);
                int percent = Mathf.RoundToInt(prob._probability * 100);

                if (prob._probability < 1)
                {
                    allDes.Add($"({percent}%) {temp}");
                }
                else
                {
                    allDes.Add(temp);
                }

            }
            string temp2 = string.Join(",", allDes);

            _choiceName.text = choiceData._choiceName;
            _choiceDesc.text = temp2;

            if (!ConditionChecker.IsChoiceAvailable(choiceData))
            {
                _button.interactable = false;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_nameScrollLength > 0 && _nameScrollCo == null)
            {
                _nameScrollCo = StartCoroutine(ScrollTextCo(_nameTextTransform, _nameDefaultX, _nameScrollLength));
            }

            if (_descScrollLength > 0 && _descScrollCo == null)
            {
                _descScrollCo = StartCoroutine(ScrollTextCo(_descTextTransform, _descDefaultX, _descScrollLength));
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_nameScrollCo != null)
            {
                StopCoroutine(_nameScrollCo);
                _nameScrollCo = null;
            }

            if (_descScrollCo != null)
            {
                StopCoroutine(_descScrollCo);
                _descScrollCo = null;
            }

            _nameTextTransform.anchoredPosition = new Vector2(_nameDefaultX, _nameTextTransform.anchoredPosition.y);
            _descTextTransform.anchoredPosition = new Vector2(_descDefaultX, _descTextTransform.anchoredPosition.y);
        }

        public void OnClickChoice()
        {
            if (_choiceData == null)
                return;

            ChoiceDataSO data = _choiceData;
            float random = Random.value;
            float temp = 0f;

            if (data._probabilisticEffect == null || data._probabilisticEffect.Count == 0)
            {
                UIManager.Instance.ScriptEventUI.HideUI();
                return;
            }

            foreach (var prob in data._probabilisticEffect)
            {
                temp += prob._probability;

                if (random <= temp)
                {
                    foreach (var effect in prob._effects)
                    {
                        EffectExecutor.ExecuteEffect(effect);
                    }

                    break;
                }
            }

            UIManager.Instance.ScriptEventUI.HideUI();
        }

        private string GetEffectText(EventEffect eventEffect)
        {
            switch (eventEffect._effectType)
            {
                case EffectType.ChangeResource:
                    if (eventEffect._value >= 0)
                        return $"{eventEffect._value}��ŭ {eventEffect._target}�� �����մϴ�";
                    else
                        return $"{eventEffect._value}��ŭ {eventEffect._target}�� �����մϴ�";
                    break;

                case EffectType.ChangeMaxHP:
                    if (eventEffect._value >= 0)
                        return $"�ִ� ü���� {eventEffect._value}��ŭ �����մϴ�";
                    else
                        return $"�ִ� ü���� {eventEffect._value}��ŭ �����մϴ�";
                    break;
                case EffectType.ChangeCurrentHP:
                    if (eventEffect._value >= 0)
                        if (eventEffect._value > PlayerManager.PlayerStatus.maxHP)
                            return $"ü���� �ִ� ü�¸�ŭ �����մϴ�";
                        else
                            return $"ü���� {eventEffect._value}��ŭ �����մϴ�";
                    else
                        return $"ü���� {eventEffect._value}��ŭ �����մϴ�";
                    break;
                case EffectType.ChangeMaxFuel:
                    if (eventEffect._value >= 0)
                        return $"�ִ� ���ᰡ {eventEffect._value}��ŭ �����մϴ�";
                    else
                        return $"�ִ� ���ᰡ {eventEffect._value}��ŭ �����մϴ�";
                    break;
                case EffectType.ChangeCurrentFuel:
                    if (eventEffect._value >= 0)
                        return $"���ᰡ {eventEffect._value}��ŭ �����մϴ�";
                    else
                        return $"���ᰡ {eventEffect._value}��ŭ �����մϴ�";
                case EffectType.ChangeRelic:
                    if (eventEffect._value >= 0)
                        return $"{eventEffect._relicData._relicName} ������ ȹ���Ͽ����ϴ�";
                    else
                        return $"{eventEffect._relicData._relicName} ������ �ı��Ǿ����ϴ�";
                    break;
                case EffectType.None:
                    return "�ƹ� ��ȭ�� �����ϴ�";
                    break;
                default:
                    return "�ƹ� ��ȭ�� �����ϴ�";
            }
        }

        private IEnumerator ScrollTextCo(RectTransform textTransform, float defaultX, float scrollLength)
        {
            while (true)
            {
                yield return null;

                while (textTransform.anchoredPosition.x > defaultX - scrollLength)
                {
                    textTransform.anchoredPosition -= new Vector2(_scrollSpeed * Time.deltaTime, 0);
                    yield return null;
                }

                yield return new WaitForSeconds(_pauseTime);

                textTransform.anchoredPosition = new Vector2(defaultX, textTransform.anchoredPosition.y);

                yield return new WaitForSeconds(_pauseTime);
            }
        }
    }
}
