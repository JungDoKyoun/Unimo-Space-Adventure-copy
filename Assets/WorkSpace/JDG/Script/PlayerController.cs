using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JDG;

namespace JDG
{
    public class PlayerController : MonoBehaviour
    {
        [Header("플레이어 이동 관련")]
        [SerializeField] private float _moveSpeed;

        private HexGridLayout _hexGridLayout;
        private Vector3 _targetPos;
        private bool _isMoving = false;

        private void Update()
        {
            MoveAction();
        }

        public bool IsMoving => _isMoving;

        public void Init(HexGridLayout hexGrid)
        {
            _hexGridLayout = hexGrid;
        }

        public void MoveTo(Vector3 targetPos)
        {
            if(!_isMoving)
            {
                _targetPos = targetPos + Vector3.up;
                _isMoving = true;
            }
        }

        private void MoveAction()
        {
            if(_isMoving)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetPos, _moveSpeed * Time.deltaTime);

                if(Vector3.Distance(transform.position, _targetPos) < 0.01f)
                {
                    transform.position = _targetPos;
                    _isMoving = false;

                    Vector2Int newCoord = _hexGridLayout.GetCoordinateFromPosition(transform.position);
                    _hexGridLayout.SetPlayerCoord(newCoord);

                    _hexGridLayout.UpdateFog();
                }
            }
        }
    }
}
