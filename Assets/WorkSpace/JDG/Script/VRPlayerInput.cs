using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JDG;
using UnityEngine.XR.Interaction.Toolkit;

namespace JDG
{
    public class VRPlayerInput : MonoBehaviour
    {
        [Header("XR Ray")]
        [SerializeField] private XRRayInteractor _rayInteractor;

        [Header("입력 버튼")]
        [SerializeField] private ActionBasedController _inputController;
        [SerializeField] private float _inputThreshold = 0.1f;

        private PlayerController _playerController;
        private HexGridLayout _hexGridLayout;

        private void Update()
        {
            TryMovePlayer();
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

        private void TryMovePlayer()
        {
            if (_rayInteractor == null || _inputController == null || _hexGridLayout == null)
                return;

            RaycastHit hit;

            if(_rayInteractor.TryGetCurrent3DRaycastHit(out hit))
            {
                Vector3 hitPos = hit.point;
                Vector2Int hitcoord = _hexGridLayout.GetCoordinateFromPosition(hitPos);

                if (_hexGridLayout.TryGetTile(hitcoord, out var tile))
                {
                    Debug.Log(tile.TileVisibility);
                    if (IsTriggerPressed())
                    {
                        if (tile.TileVisibility == TileVisibility.Visible)
                        {
                            Vector3 target = _hexGridLayout.GetPositionForHexFromCoordinate(hitcoord);
                            _playerController.MoveTo(target + Vector3.up * 1f);
                        }
                    }
                }
            }
        }
    }
}
