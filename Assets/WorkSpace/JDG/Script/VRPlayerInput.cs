using UnityEngine;
using UnityEngine.UI;
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

            //float triggerValue = _inputController.selectAction.action.ReadValue<float>();
            //return triggerValue > _inputThreshold;
            return _inputController.selectAction.action.triggered;
        }

        private void TryRayInteract()
        {
            Debug.Log("들어왔다1");
            if (_rayInteractor == null || _inputController == null || _hexGridLayout == null)
                return;

            RaycastHit hit;

            if (_rayInteractor.TryGetCurrent3DRaycastHit(out hit))
            {
                GameObject hitObj = hit.collider.gameObject;

                if (hitObj.TryGetComponent<Button>(out Button button))
                {
                    if (IsTriggerPressed())
                    {
                        button.onClick.Invoke();
                        return;
                    }
                }

                Vector3 hitPos = hit.point;
                Vector2Int hitcoord = _hexGridLayout.GetCoordinateFromPosition(hitPos);
                Debug.Log("들어왔다2");
                //if (_hexGridLayout.TryGetTile(hitcoord, out var tile))
                //{
                if (IsTriggerPressed())
                {
                    if (_tileSelectionUI != null && _tileSelectionUI.IsUIOpen)
                        return;

                    //if (tile.TileData.TileVisibility == TileVisibility.Visible)
                    //{
                    //    Vector3 center = _hexGridLayout.GetPositionForHexFromCoordinate(_hexGridLayout.GetBaseCoord());;
                    //    _tileSelectionUI.ShowUI(tile, center);
                    //    return;
                    //}

                    if (hit.collider.TryGetComponent<WorldMapRenderer>(out WorldMapRenderer renderer))
                    {
                        Debug.Log("들어옴");
                        renderer.HandleRayHit(hit);
                        return;
                    }
                }
                //}
            }
        }
    }
}
