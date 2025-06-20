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
        [SerializeField] private int _moveAmount;

        private HexGridLayout _hexGridLayout;
        private Vector3 _targetPos;
        private bool _isMoving = false;

        public int ViewRange { get { return _viewRange; } set { _viewRange = value; } }
        public bool IsMoving => _isMoving;

        public void Init(HexGridLayout hexGrid)
        {
            _hexGridLayout = hexGrid;
            Vector2Int coord = _hexGridLayout.GetCoordinateFromPosition(transform.position);
            _hexGridLayout.UpdateFog(ViewRange);
            _hexGridLayout.ShowMoveableTile(coord, _moveAmount);
        }

        public void MoveTo(Vector2Int target)
        {
            if (_isMoving)
                return;

            Vector2Int playerCoord = _hexGridLayout.GetCoordinateFromPosition(transform.position);
            List<Vector2Int> paths = _hexGridLayout.FindPath(playerCoord, target);

            if (paths == null || paths.Count == 0)
                return;

            StartCoroutine(MoveAction(paths));
        }

        private IEnumerator MoveAction(List<Vector2Int> paths)
        {
            _isMoving = true;

            foreach(var path in paths)
            {
                Vector3 targetPos = _hexGridLayout.GetPositionForHexFromCoordinate(path) + Vector3.up;
                Vector3 dir = (targetPos - transform.position).normalized;
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

                while(Vector3.Distance(transform.position, targetPos) > 0.01f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetPos, _moveSpeed * Time.deltaTime);

                    yield return null;
                }

                transform.position = targetPos;

                UpdateFog();
            }

            _isMoving = false;
            Vector2Int playerCoord = _hexGridLayout.PlayerCoord;
            _hexGridLayout.ShowMoveableTile(playerCoord, _moveAmount);
        }

        public void UpdateFog()
        {
            Vector2Int newCoord = _hexGridLayout.GetCoordinateFromPosition(transform.position);
            _hexGridLayout.SetPlayerCoord(newCoord);
            _hexGridLayout.UpdateFog(_viewRange);
        }
    }
}
