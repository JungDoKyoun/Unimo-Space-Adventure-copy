using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JDG;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

namespace JDG
{
    public class VRPlayerInput : MonoBehaviour
    {
        [Header("XR Ray")]
        [SerializeField] private XRRayInteractor _rayInteractor;

        [Header("입력 버튼")]
        [SerializeField] private ActionBasedController _inputController;
        [SerializeField] private float _inputThreshold = 0.1f;

        [Header("UI 관련")]
        [SerializeField] TileSelectionUI _tileSelectionUI;

        private PlayerController _playerController;
        private HexGridLayout _hexGridLayout;

        private void Update()
        {
            TryRayInteract();
        }

        public void Init(PlayerController playerController, HexGridLayout hexGridLayout)
        {
            _playerController = playerController;
            _hexGridLayout = hexGridLayout;
        }

        private bool IsTriggerPressed()
        {
            if (_inputController.selectAction == null || _inputController.selectAction.action == null)
                return false;

            float triggerValue = _inputController.selectAction.action.ReadValue<float>();
            return triggerValue > _inputThreshold;
        }

        private void TryRayInteract()
        {
            if (_rayInteractor == null || _inputController == null || _hexGridLayout == null)
                return;

            RaycastHit hit;

            if(_rayInteractor.TryGetCurrent3DRaycastHit(out hit))
            {
                GameObject hitObj = hit.collider.gameObject;

                if(hitObj.TryGetComponent<Button>(out Button button))
                {
                    if(IsTriggerPressed())
                    {
                        button.onClick.Invoke();
                        return;
                    }
                }

                Vector3 hitPos = hit.point;
                Vector2Int hitcoord = _hexGridLayout.GetCoordinateFromPosition(hitPos);

                if (_hexGridLayout.TryGetTile(hitcoord, out var tile))
                {
                    if (IsTriggerPressed())
                    {
                        if (tile.TileData.TileVisibility == TileVisibility.Visible)
                        {
                            Vector3 center = _hexGridLayout.GetPositionForHexFromCoordinate(_hexGridLayout.GetBaseCoord());;
                            _tileSelectionUI.ShowUI(tile, center);
                            return;
                        }
                    }
                }
            }
        }
    }
}
