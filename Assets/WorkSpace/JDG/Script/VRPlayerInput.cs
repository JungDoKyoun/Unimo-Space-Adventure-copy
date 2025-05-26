using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using JDG;

namespace JDG
{
    public class VRPlayerInput : MonoBehaviour
    {
        [Header("XR Ray")]
        [SerializeField] private XRRayInteractor _rightRayInteractor;
        [SerializeField] private XRRayInteractor _leftRayInteractor;

        [Header("입력 버튼")]
        [SerializeField] private ActionBasedController _rightInputController;
        [SerializeField] private ActionBasedController _leftInputController;

        [Header("UI 관련")]
        TileSelectionUI _tileSelectionUI;

        private PlayerController _playerController;
        private HexGridLayout _hexGridLayout;

        private void Update()
        {
            TryRayInteract(_rightRayInteractor, _rightInputController);
            TryRayInteract(_leftRayInteractor, _leftInputController);
        }

        public void Init(PlayerController playerController, HexGridLayout hexGridLayout)
        {
            _playerController = playerController;
            _hexGridLayout = hexGridLayout;
            _tileSelectionUI = UIManager.Instance.TileSelectionUI;
        }

        private bool IsTriggerPressed(ActionBasedController controller)
        {
            if (controller?.activateAction.action == null)
                return false;

            return controller.activateAction.action.triggered;
        }

        private void TryRayInteract(XRRayInteractor rayInteractor, ActionBasedController inputController)
        {
            if (rayInteractor == null || inputController == null || _hexGridLayout == null)
                return;

            RaycastHit hit;

            if (_rightRayInteractor.TryGetCurrent3DRaycastHit(out hit))
            {
                //GameObject hitObj = hit.collider.gameObject;

                //if (hitObj.TryGetComponent<Button>(out Button button))
                //{
                //    if (IsTriggerPressed(inputController))
                //    {
                //        button.onClick.Invoke();
                //        return;
                //    }
                //}

                Vector3 hitPos = hit.point;
                Vector2Int hitcoord = _hexGridLayout.GetCoordinateFromPosition(hitPos);

                if (IsTriggerPressed(inputController))
                {
                    if (_tileSelectionUI != null && UIManager.Instance.IsUIOpen)
                        return;

                    if (hit.collider.TryGetComponent<WorldMapRenderer>(out WorldMapRenderer renderer))
                    {
                        renderer.HandleRayHit(hit);
                        return;
                    }
                }
            }
        }
    }
}
