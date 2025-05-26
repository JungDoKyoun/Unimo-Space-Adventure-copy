using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
        private GameObject _rewardPrefab;
        [SerializeField] private Button _actionButton;
        [SerializeField] private TextMeshProUGUI _actionButtonName;

        [Header("UI 오프셋")]
        [SerializeField] private Vector3 _offSet;

        [Header("필요한 컴포넌트")]
        [SerializeField] private Camera _worldCam;
        [SerializeField] private EventTileConfig _eventTileConfig;
        [SerializeField] private ShopUI _shopUI;

        private Vector3 _uiPos;
        private HexRenderer _currentTile;
        private PlayerController _playerController;
        private HexGridLayout _hexGrid;
        private bool _isUIOpen = false;

        private void Start()
        {
            HideUI();
        }

        public bool IsUIOpen => _isUIOpen;

        public void Init(PlayerController playerController, HexGridLayout hexGird)
        {
            _playerController = playerController;
            _hexGrid = hexGird;
        }

        public void ShowUI(HexRenderer tile, Vector3 wordPos = default)
        {
            _currentTile = tile;
            transform.position = wordPos + _offSet;
            _uiPos = wordPos;
            _root.SetActive(true);
            _isUIOpen = true;

            var env = TileEnvironmentManager.Instance.GetEnvironmentInfo(tile.TileData.EnvironmentType);
            var display = TileDisplayInfoManager.Instance.GetDisplayInfo(tile.TileData.TileType, tile.TileData.ModeType);
            var rewards = RewardManager.Instance.GetTileRewardRuleSO(tile.TileData.TileType, tile.TileData.ModeType);
            if (env != null)
            {
                _envImage.sprite = env.EnviromentIcon;
                _envText.text = env.EnviromentName;
            }

            if (display != null)
            {
                _displayImage.sprite = display.DisplayIcon;
                _displayText.text = display.DisplayName;
            }

            foreach (Transform child in _rewardParent)
            {
                Destroy(child.gameObject);
            }

            _rewardPrefab = Resources.Load<GameObject>("WorldMap/RewardSlot");

            foreach (RewardData reward in rewards)
            {
                GameObject obj = Instantiate(_rewardPrefab, _rewardParent);
                RewardSlot rewardSlot = obj.GetComponent<RewardSlot>();

                if (rewardSlot != null)
                {
                    rewardSlot.SetRewardSlot(reward.RewardIcon, reward.RewardName, reward.RewardAmount);
                }
            }

            if (tile.TileData.IsCleared || tile.TileData.TileType == TileType.Event || tile.TileData.TileType == TileType.Base)
            {
                _actionButtonName.text = "a";
            }
            else
            {
                _actionButtonName.text = "시작";
            }
        }

        public void HideUI()
        {
            _root.SetActive(false);
            _isUIOpen = false;
        }

        private void MovePlayerTo(HexRenderer tile)
        {
            Vector3 target = _hexGrid.GetPositionForHexFromCoordinate(tile.TileData.Coord);
            _playerController.MoveTo(target);
            _hexGrid.UpdateFog();
        }

        public void OnActionButtonClicked(int itmeSlot = 0)
        {
            if (_currentTile == null)
                return;

            var tile = _currentTile.TileData;

            if (tile.IsCleared || tile.TileType == TileType.Base)
            {
                MovePlayerTo(_currentTile);
            }

            else if (_currentTile.TileData.TileType == TileType.Event)
            {
                //나중에 이벤트 발동 함수 넣으면됨
                _currentTile.TileData.IsCleared = true;
                MovePlayerTo(_currentTile);

                if (_currentTile.TileData.EventType == EventType.Shop)
                {
                    EventDataSO eventData = GetRandomEvent(EventType.Shop);
                    List<RelicDataSO> relicDatas = GetRandomRelics(eventData._relicDatas, itmeSlot);
                    StartCoroutine(WaitAndOpenShop(relicDatas));
                }
            }
            else
            {
                SceneLoader.Instance.EnterTileScene(_currentTile);
            }

            HideUI();
        }

        public void OnCancleButtonClicked()
        {
            HideUI();
            _isUIOpen = false;
        }

        private EventDataSO GetRandomEvent(EventType type)
        {
            foreach (var eve in _eventTileConfig._eventTypes)
            {
                if (eve._eventType == type && eve._eventData.Count > 0)
                {
                    int random = Random.Range(0, eve._eventData.Count);
                    return eve._eventData[random];
                }
            }
            return null;
        }

        private List<RelicDataSO> GetRandomRelics(List<RelicDataSO> relicDatas, int count)
        {
            List<RelicDataSO> copy = new List<RelicDataSO>(relicDatas);
            List<RelicDataSO> result = new List<RelicDataSO>();

            int maxCount = Mathf.Min(count, copy.Count);

            for (int i = 0; i < maxCount; i++)
            {
                int random = Random.Range(0, copy.Count);
                result.Add(copy[random]);
                copy.RemoveAt(random);
            }
            return result;
        }

        private IEnumerator WaitAndOpenShop(List<RelicDataSO> relics)
        {
            yield return new WaitUntil(() => !_playerController.IsMoving);

            _shopUI.OpenShopUI(relics, _uiPos);
            _shopUI.OnShopClosed -= ClearUIFlag;
            _shopUI.OnShopClosed += ClearUIFlag;
        }

        private void ClearUIFlag()
        {
            _isUIOpen = false;
        }
    }
}