using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JDG;
using TMPro;
using UnityEngine.UI;

namespace JDG
{
    public class TileSelectionUI : MonoBehaviour
    {
        [Header("각종 버튼 모음")]
        [SerializeField] private GameObject _root;
        [SerializeField] private Image _envImage;
        [SerializeField] private TextMeshProUGUI _envText;
        [SerializeField] private Image _displayImage;
        [SerializeField] private TextMeshProUGUI _displayText;
        [SerializeField] private Transform _rewardParent;
        [SerializeField] private GameObject _rewardPrefab;
        [SerializeField] private Button _actionButton;
        [SerializeField] private TextMeshProUGUI _actionButtonName;

        [Header("UI 오프셋")]
        [SerializeField] private Vector3 _offSet;

        private HexRenderer _currentTile;
        private PlayerController _playerController;
        private HexGridLayout _hexGrid;

        private void Start()
        {
            HideUI();
        }

        public void Init(PlayerController playerController, HexGridLayout hexGird)
        {
            _playerController = playerController;
            _hexGrid = hexGird;
        }

        public void ShowUI(HexRenderer tile, Vector3 wordPos)
        {
            _currentTile = tile;
            transform.position = wordPos + _offSet;
            _root.SetActive(true);

            var env = TileEnvironmentManager.Instance.GetEnvironmentInfo(tile.TileData.EnvironmentType);
            var display = TileDisplayInfoManager.Instance.GetDisplayInfo(tile.TileData.TileType, tile.TileData.ModeName);
            var rewards = RewardManager.Instance.GetTileRewardRuleSO(tile.TileData.TileType, tile.TileData.ModeName);

            if(env != null)
            {
                _envImage.sprite = env.EnviromentIcon;
                _envText.text = env.EnviromentName;
            }

            if(display != null)
            {
                _displayImage.sprite = display.DisplayIcon;
                _displayText.text = display.DisplayName;
            }

            foreach(Transform child in _rewardParent)
            {
                Destroy(child.gameObject);
            }

            foreach(RewardData reward in rewards)
            {
                GameObject obj = Instantiate(_rewardPrefab, _rewardParent);
                RewardSlot rewardSlot = obj.GetComponent<RewardSlot>();

                if(rewardSlot != null)
                {
                    rewardSlot.SetRewardSlot(reward.RewardIcon, reward.RewardName, reward.RewardAmount);
                }
            }

            if(tile.TileData.IsCleared || tile.TileData.TileType == TileType.Event)
            {
                _actionButtonName.text = "Move";
            }
            else
            {
                _actionButtonName.text = "Play";
            }
        }

        public void HideUI()
        {
            _root.SetActive(false);
        }

        private void MovePlayerTo(HexRenderer tile)
        {
            Vector3 target = _hexGrid.GetPositionForHexFromCoordinate(tile.TileData.Coord);
            _playerController.MoveTo(target);
        }

        public void OnActionButtonClicked()
        {
            if (_currentTile == null)
                return;

            if(_currentTile.TileData.IsCleared || _currentTile.TileData.TileType == TileType.Event)
            {
                if(_currentTile.TileData.TileType == TileType.Event)
                {
                    //나중에 이벤트 발동 함수 넣으면됨
                    Debug.Log("이벤트 실행됨");
                    _currentTile.TileData.IsCleared = true;
                }
                MovePlayerTo(_currentTile);
            }
            else
            {
                Debug.Log(_currentTile);
                SceneLoader.Instance.EnterTileScene(_currentTile);
            }

            HideUI();
        }

        public void OnCancleButtonClicked()
        {
            HideUI();
        }
    }
}