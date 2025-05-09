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
        [SerializeField] private InputHelpers.Button _inputButton = InputHelpers.Button.TriggerButton;
        [SerializeField] private float _inputThreshold = 0.1f;

        private PlayerController _playerController;
        private HexGridLayout _hexGridLayout;

        private void Update()
        {
            
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
    }
}
