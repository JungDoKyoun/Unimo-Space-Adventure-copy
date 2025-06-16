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

        [Header("레이저 포인터 관련")]
        [SerializeField] private float _pointScale;
        private GameObject _redRayHitPointPrefab;
        private GameObject _redRightRayHitPointInstance;
        private GameObject _redLeftRayHitPointInstance;

        private PlayerController _playerController;
        private HexGridLayout _hexGridLayout;

        private void Update()
        {
            ShowAllPoint();
            PressButton();
        }

        public void Init(PlayerController playerController, HexGridLayout hexGridLayout)
        {
            _playerController = playerController;
            _hexGridLayout = hexGridLayout;
            _redRayHitPointPrefab = Resources.Load<GameObject>("WorldMap/RedRayHitPoint");
            _redRightRayHitPointInstance = Instantiate(_redRayHitPointPrefab);
            _redRightRayHitPointInstance.transform.localScale = new Vector3(_pointScale, _pointScale, 0);
            _redRightRayHitPointInstance.transform.position = new Vector3(-100, -100, -100);
            _redRightRayHitPointInstance.SetActive(false);
            _redLeftRayHitPointInstance = Instantiate(_redRayHitPointPrefab);
            _redLeftRayHitPointInstance.transform.localScale = new Vector3(_pointScale, _pointScale, 0);
            _redLeftRayHitPointInstance.SetActive(false);
            _redLeftRayHitPointInstance.transform.position = new Vector3(-100, -100, -100);
        }

        private bool IsTriggerPressed(ActionBasedController controller)
        {
            if (controller?.activateAction.action == null)
                return false;

            return controller.activateAction.action.WasPressedThisFrame();
        }

        private void TryRayInteract(XRRayInteractor rayInteractor)
        {
            if (rayInteractor == null || _hexGridLayout == null)
                return;

            RaycastHit hit;

            if (rayInteractor.TryGetCurrent3DRaycastHit(out hit))
            {
                Vector3 hitPos = hit.point;
                Vector2Int hitcoord = _hexGridLayout.GetCoordinateFromPosition(hitPos);

                if (hit.collider.TryGetComponent<WorldMapRenderer>(out WorldMapRenderer renderer))
                {
                    renderer.HandleRayHit(hit);
                    return;
                }
            }
        }

        private void PressButton()
        {
            if (UIManager.Instance.IsUIOpen)
                return;

            if (IsTriggerPressed(_rightInputController))
            {
                TryRayInteract(_rightRayInteractor);
            }

            else if (IsTriggerPressed(_leftInputController))
            {
                TryRayInteract(_leftRayInteractor);
            }
        }

        private void ShowRayPoin(XRRayInteractor rayInteractor, GameObject pointInstance)
        {
            if (rayInteractor == null)
                return;

            RaycastHit hit;

            if(rayInteractor.TryGetCurrent3DRaycastHit(out hit))
            {
                if(hit.collider.GetComponent<WorldMapRenderer>())
                {
                    pointInstance.SetActive(true);
                    Vector3 hitPos = hit.point - new Vector3(0, 0, 0.1f);
                    pointInstance.transform.position = hitPos;
                }
                else
                {
                    pointInstance.SetActive(false);
                }
            }
            else
            {
                pointInstance.SetActive(false);
            }
        }

        private void ShowAllPoint()
        {
            if (UIManager.Instance.IsUIOpen)
            {
                _redLeftRayHitPointInstance.SetActive(false);
                _redRightRayHitPointInstance.SetActive(false);
                return;
            }

            ShowRayPoin(_rightRayInteractor, _redRightRayHitPointInstance);
            ShowRayPoin(_leftRayInteractor, _redLeftRayHitPointInstance);
        }
    }
}
