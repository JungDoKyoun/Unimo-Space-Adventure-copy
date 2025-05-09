using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace JDG
{
    public class HexGridLayout : MonoBehaviour
    {
        [Header("그리드 세팅")]
        [SerializeField] private Vector2Int _gridSize;

        [Header("타일 세팅")]
        [SerializeField] private int _spawnCount = 80;
        [SerializeField] private float _outerSize = 1f;
        [SerializeField] private float _innerSize = 0f;
        [SerializeField] private float _height = 1f;
        [SerializeField] private Material _material;

        [Header("플레이어 관련")]
        [SerializeField] private VRPlayerInput _vRPlayerInput;
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private int _viewRange = 1;
        private GameObject _playerInstance;

        private List<Vector2Int> _tileCoords = new List<Vector2Int>();
        private Vector2Int _playerCoord;
        private Dictionary<Vector2Int, HexRenderer> _hexMap = new Dictionary<Vector2Int, HexRenderer>();

        private void OnEnable()
        {
            GenerateConnectedMap();
            LayoutGrid();
        }

        private void OnValidate()
        {
            if (Application.isPlaying)
            {
                GenerateConnectedMap();
                LayoutGrid();
            }
        }

        private void GenerateConnectedMap()
        {
            Vector2Int center = new Vector2Int(_gridSize.x / 2, _gridSize.y / 2);
            _tileCoords.Clear();
            _tileCoords.Add(center);
            _playerCoord = center;

            List<Vector2Int> frontier = new List<Vector2Int>(GetNeighbors(center));

            while(_tileCoords.Count <= _spawnCount && frontier.Count > 0)
            {
                Vector2Int current = frontier[Random.Range(0, frontier.Count)];
                frontier.Remove(current);

                if(_tileCoords.Contains(current))
                {
                    continue;
                }

                _tileCoords.Add(current);

                foreach(var neighbor in GetNeighbors(current))
                {
                    if(!frontier.Contains(neighbor) && !_tileCoords.Contains(neighbor))
                    {
                        frontier.Add(neighbor);
                    }
                }
            }
        }

        private List<Vector2Int> GetNeighbors(Vector2Int coord)
        {
            List<Vector2Int> result = new List<Vector2Int>();
            int x = coord.x;
            int y = coord.y;

            Vector2Int[] evenOffsets =
            {
                new(x + 1, y), new(x, y + 1), new(x - 1, y + 1), new(x - 1, y), new(x - 1, y - 1), new(x, y - 1)
            };

            Vector2Int[] oddOffsets =
            {
                new(x + 1, y), new(x + 1, y + 1), new(x, y + 1), new(x - 1, y), new Vector2Int(x, y -1), new(x + 1, y - 1)
            };

            var offsets = (y % 2 == 0) ? evenOffsets : oddOffsets;

            foreach (var pos in offsets)
            {
                if (pos.x >= 0 && pos.x < _gridSize.x && pos.y >= 0 && pos.y < _gridSize.y)
                {
                    result.Add(pos);
                }
            }

            return result;
        }

        private void LayoutGrid()
        {
            _hexMap.Clear();

            foreach(var coord in _tileCoords)
            {
                GameObject tile = new GameObject($"Hex {coord.x},{coord.y}", typeof(HexRenderer));
                tile.transform.position = GetPositionForHexFromCoordinate(coord);

                HexRenderer hexRenderer = tile.GetComponent<HexRenderer>();
                hexRenderer.OuterSize = _outerSize;
                hexRenderer.InnerSize = _innerSize;
                hexRenderer.Height = _height;
                hexRenderer.SetMaterial(_material);
                hexRenderer.DrawMesh();
                hexRenderer.SetVisibility(TileVisibility.Hidden);
                _hexMap.Add(coord, hexRenderer);

                tile.transform.SetParent(transform, true);
            }

            Vector3 spawnPos = GetPositionForHexFromCoordinate(_playerCoord) + Vector3.up * 1f;
            _playerInstance = Instantiate(_playerPrefab, spawnPos, Quaternion.identity);

            var player = _playerInstance.GetComponent<PlayerController>();
            player.Init(this);

            if(_vRPlayerInput != null)
            {
                _vRPlayerInput.Init(player, this);
            }
            
            UpdateFog();
        }

        public Vector3 GetPositionForHexFromCoordinate(Vector2Int coordinate)
        {
            int column = coordinate.x;
            int row = coordinate.y;
            float width;
            float height;
            float xPosition;
            float yPosition;
            bool shouldOffset;
            float horizontalDistance;
            float verticalDistance;
            float offset;
            float size = _outerSize;

            shouldOffset = (row % 2) == 0;
            width = Mathf.Sqrt(3) * size;
            height = 2f * size;

            horizontalDistance = width;
            verticalDistance = height * (3f / 4f);

            offset = (shouldOffset) ? 0 : width / 2;

            xPosition = (column * horizontalDistance) + offset;
            yPosition = (row * verticalDistance);

            return new Vector3(xPosition, 0, -yPosition);
        }

        private int HexDistance(Vector2Int a, Vector2Int b)
        {
            int ax = a.x - (a.y - (a.y & 1)) / 2;
            int az = a.y;
            int ay = -ax - az;

            int bx = b.x - (b.y - (b.y & 1)) / 2;
            int bz = b.y;
            int by = -bx - bz;

            return Mathf.Max(Mathf.Abs(ax - bx), Mathf.Abs(ay - by), Mathf.Abs(az - bz));
        }

        public void UpdateFog()
        {
            Debug.Log("안개 실행");
            foreach(var pair in _hexMap)
            {
                Vector2Int coord = pair.Key;
                HexRenderer hex = pair.Value;

                var dis = HexDistance(coord, _playerCoord);

                if(dis <= _viewRange)
                {
                    hex.SetVisibility(TileVisibility.Visible);
                }
                else if(hex.GetTileVisibility() == TileVisibility.Visible)
                {
                    hex.SetVisibility(TileVisibility.Visited);
                }
            }
        }

        public Vector2Int GetCoordinateFromPosition(Vector3 pos)
        {
            float size = _outerSize;
            float width = Mathf.Sqrt(3) * size;
            float height = 2f * size;
            float verticalDistance = height * (3 / 4);

            int row = Mathf.RoundToInt(-pos.z / verticalDistance);
            float offset = (row % 2 == 0) ? 0 : width / 2;
            int column = Mathf.RoundToInt((pos.x - offset) / width);

            return new Vector2Int(column, row);
        }

        public void SetPlayerCoord(Vector2Int newCoord)
        {
            _playerCoord = newCoord;
        }

        public bool TryGetTile(Vector2Int coord, out HexRenderer hex)
        {
            return _hexMap.TryGetValue(coord, out hex);
        }
    }
}
