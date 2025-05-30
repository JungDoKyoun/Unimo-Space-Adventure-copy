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
        [SerializeField] private RectTransform _textTransform;
        [SerializeField] private RectTransform _maskAreaTransform;
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _choiceText;
        [SerializeField] private Sprite _buttonUsefulImage;
        [SerializeField] private Sprite _buttonHarmfulImage;
        [SerializeField] private Sprite _buttonUseAndHarmfulImage;

        [Header("글자 속도 조정")]
        [SerializeField] private float _scrollSpeed;
        [SerializeField] private float _pauseTime;

        private ChoiceDataSO _choiceData;
        private Coroutine _scrollCo;
        private float _defaultX;
        private float _scrollLength;
        private bool _isScroll = false;

        private void Start()
        {
            _defaultX = _textTransform.anchoredPosition.x;
            var tmp = _textTransform.GetComponent<TextMeshProUGUI>();
            float textLength = tmp.preferredWidth;
            float maskAreaLength = _maskAreaTransform.rect.width;
            _scrollLength = Mathf.Max(0, textLength - maskAreaLength);
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

            _choiceText.text = $"{choiceData._choiceName} : {choiceData._choiceDesc}";
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_scrollLength > 0 && !_isScroll)
            {
                _scrollCo = StartCoroutine(ScrollTextCo());
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(_scrollCo != null)
            {
                StopCoroutine(_scrollCo);
                _scrollCo = null;
            }

            _isScroll = false;
            _textTransform.anchoredPosition = new Vector2(_defaultX, _textTransform.anchoredPosition.y);
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

        private IEnumerator ScrollTextCo()
        {
            while(true)
            {
                yield return null;
                _isScroll = true;

                while (_textTransform.anchoredPosition.x > _defaultX - _scrollLength)
                {
                    _textTransform.anchoredPosition -= new Vector2(_scrollSpeed * Time.deltaTime, 0);
                    yield return null;
                }

                yield return new WaitForSeconds(_pauseTime);

                _isScroll = false;
                _textTransform.anchoredPosition = new Vector2(_defaultX, _textTransform.anchoredPosition.y);

                yield return new WaitForSeconds(_pauseTime);
            }
        }
    }
}
