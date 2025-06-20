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
        [SerializeField] private int _viewRange;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private int _moveAmonut;

        private HexGridLayout _hexGridLayout;
        private Vector3 _targetPos;
        private bool _isMoving = false;

        public int ViewRange { get { return _viewRange; } set { _viewRange = value; } }
        public bool IsMoving => _isMoving;

        public void Init(HexGridLayout hexGrid)
        {
            _hexGridLayout = hexGrid;
            Vector2Int coord = _hexGridLayout.GetCoordinateFromPosition(transform.position);
            _hexGridLayout.ShowMoveableTile(coord, _moveAmonut);
        }

        public void MoveTo(Vector3 targetPos)
        {
            if(!_isMoving)
            {
                _targetPos = targetPos + Vector3.up;
                _isMoving = true;
                StartCoroutine(MoveAction());
            }
        }

        private IEnumerator MoveAction()
        {
            if(_isMoving)
            {
                Vector3 dir = _targetPos - transform.position;
                dir.y = 0;

                if(dir != Vector3.zero)
                {
                    Quaternion targetRo = Quaternion.LookRotation(dir);

                    while(Quaternion.Angle(transform.rotation, targetRo) > 0.5f)
                    {
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRo, _rotationSpeed * Time.deltaTime);

                        yield return null;
                    }

                    transform.rotation = targetRo;
                }

                while(Vector3.Distance(transform.position, _targetPos) > 0.01f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, _targetPos, _moveSpeed * Time.deltaTime);

                    yield return null;
                }

                transform.position = _targetPos;
                _isMoving = false;

                UpdateFog();
                _hexGridLayout.ShowMoveableTile(_hexGridLayout.GetCoordinateFromPosition(transform.position), _moveAmonut);
            }
        }

        public void UpdateFog()
        {
            Vector2Int newCoord = _hexGridLayout.GetCoordinateFromPosition(transform.position);
            _hexGridLayout.SetPlayerCoord(newCoord);
            _hexGridLayout.UpdateFog(_viewRange);
        }
    }
}
