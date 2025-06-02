using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JDG;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

namespace JDG
{
    public class ScriptEventChoiceShlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("슬롯 생성시 필요한 것들")]
        [SerializeField] private RectTransform _nameTextTransform;
        [SerializeField] private RectTransform _descTextTransform;
        [SerializeField] private RectTransform _maskAreaTransform;
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _choiceName;
        [SerializeField] private TextMeshProUGUI _choiceDesc;
        [SerializeField] private Sprite _buttonUsefulImage;
        [SerializeField] private Sprite _buttonHarmfulImage;
        [SerializeField] private Sprite _buttonUseAndHarmfulImage;

        [Header("글자 속도 조정")]
        [SerializeField] private float _scrollSpeed;
        [SerializeField] private float _pauseTime;

        private ChoiceDataSO _choiceData;
        private Coroutine _nameScrollCo;
        private Coroutine _descScrollCo;
        private float _nameDefaultX;
        private float _descDefaultX;
        private float _nameScrollLength;
        private float _descScrollLength;
        private bool _isNameScroll = false;
        private bool _isDescScroll = false;

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
            bool isUseful = false;
            bool isHarmful = false;

            foreach (var probEffect in choiceData._probabilisticEffect)
            {
                foreach (var effect in probEffect._effects)
                {
                    if (effect._choiceEffectType == ChoiceEffectType.Useful)
                    {

                        isUseful = true;
                    }
                    else if (effect._choiceEffectType == ChoiceEffectType.Harmful)
                    {
                        isHarmful = true;
                    }
                    else if (effect._choiceEffectType == ChoiceEffectType.None)
                    {
                        isUseful = true;
                    }

                    if (isUseful && isHarmful)
                        break;
                }

                if (isUseful && isHarmful)
                    break;
            }

            if (isUseful && isHarmful)
            {
                _button.image.sprite = _buttonUseAndHarmfulImage;
            }
            else if(isUseful)
            {
                _button.image.sprite = _buttonUsefulImage;
            }
            else if(isHarmful)
            {
                _button.image.sprite = _buttonHarmfulImage;
            }

            _choiceName.text = choiceData._choiceName;
            _choiceDesc.text = choiceData._choiceDesc;

            if(!ConditionChecker.IsChoiceAvailable(choiceData))
            {
                _button.interactable = false;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_nameScrollLength > 0 && _nameScrollCo == null)
            {
                _isNameScroll = true;
                _nameScrollCo = StartCoroutine(ScrollTextCo(_nameTextTransform, _nameDefaultX, _nameScrollLength));
            }

            if(_descScrollLength > 0 && _descScrollCo == null)
            {
                _isDescScroll = true;
                _descScrollCo = StartCoroutine(ScrollTextCo(_descTextTransform, _descDefaultX, _descScrollLength));
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(_nameScrollCo != null)
            {
                StopCoroutine(_nameScrollCo);
                _nameScrollCo = null;
            }

            if (_descScrollCo != null)
            {
                StopCoroutine(_descScrollCo);
                _descScrollCo = null;
            }

            _isNameScroll = false;
            _isDescScroll = false;
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

                if(random <= temp)
                {
                    foreach(var effect in prob._effects)
                    {
                        EffectExecutor.ExecuteEffect(effect);
                    }

                    break;
                }
            }

            UIManager.Instance.ScriptEventUI.HideUI();
        }

        private IEnumerator ScrollTextCo(RectTransform textTransform, float defaultX, float scrollLength)
        {
            while(true)
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
