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
        [SerializeField] private Sprite _buttonharmfulImage;

        [Header("글자 속도 조정")]
        [SerializeField] private float _scrollSpeed;
        [SerializeField] private float _pauseTime;

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
            if(choiceData._eventEffects._choiceEffectType == ChoiceEffectType.useful)
            {
                _button.image.sprite = _buttonUsefulImage;
            }
            else if(choiceData._eventEffects._choiceEffectType == ChoiceEffectType.harmful)
            {
                _button.image.sprite = _buttonharmfulImage;
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
