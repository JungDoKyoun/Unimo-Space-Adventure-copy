using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace JDG
{
    public enum TileType
    {
        None,
        Base,
        Boss,
        Event,
        Mode
    }

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

        [Header("타일 역할 배치 변수")]
        [SerializeField] private List<ModeRatioEntry> _modeRatio = new List<ModeRatioEntry>();
        [SerializeField] private int[] _bossDistance;
        [SerializeField] private int _bossCountPerCircle;
        [SerializeField] private int[] _bossMinGapByDistance;
        [SerializeField] private float _eventTileRatio;
        [SerializeField] private int _eventMinDistance;


        [Header("플레이어 관련")]
        [SerializeField] private VRPlayerInput _vRPlayerInput;
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private int _viewRange = 1;
        private GameObject _playerInstance;

        private List<Vector2Int> _tileCoords = new List<Vector2Int>(); //타일 좌표 리스트
        private Vector2Int _playerCoord;
        private Dictionary<Vector2Int, HexRenderer> _hexMap = new Dictionary<Vector2Int, HexRenderer>(); //타일 오브젝트 정보

        private void OnEnable()
        {
            GenerateConnectedMap();
            LayoutGrid();
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
                MeshCollider collider = tile.AddComponent<MeshCollider>();
                collider.sharedMesh = tile.GetComponent<MeshFilter>().mesh;
                //hexRenderer.SetVisibility(TileVisibility.Hidden);
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

            AssignTileRoles();
            UpdateFog();
        }

        public Vector3 GetPositionForHexFromCoordinate(Vector2Int coord)
        {
            int column = coord.x;
            int row = coord.y;
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
            float verticalDistance = height * (3f / 4f);

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
            Debug.Log(_hexMap.TryGetValue(coord, out hex));
            return _hexMap.TryGetValue(coord, out hex);
        }

        private void AssignBossTiles(List<Vector2Int> candidateCoords)
        {
            List<Vector2Int> placedBosses = new List<Vector2Int>();

            for(int i = 0; i < _bossCountPerCircle; i++)
            {
                if (i >= _bossDistance.Length || i >= _bossMinGapByDistance.Length)
                    break;

                int distance = _bossDistance[i];
                int minGap = _bossMinGapByDistance[i];

                List<Vector2Int> valid = candidateCoords.FindAll(coord =>
                HexDistance(_playerCoord, coord) >= distance &&
                !placedBosses.Exists(b => HexDistance(b, coord) < minGap));
                Debug.Log($"[보스 배치] 거리 {distance}에서 후보 {valid.Count}개");

                if (valid.Count > 0)
                {
                    int randomIndex = Random.Range(0, valid.Count);
                    var chosen = valid[randomIndex];
                    _hexMap[chosen].TileType = TileType.Boss;
                    placedBosses.Add(chosen);
                    candidateCoords.Remove(chosen);
                }

                else
                {
                    Debug.LogWarning($"[보스 배치 실패] 거리 {distance}에 유효한 후보가 없음");
                }
            }
        }

        private void AssignEventTiles(List<Vector2Int> candidateCoords)
        {
            int eventCount = Mathf.Max(_eventMinDistance, Mathf.RoundToInt(candidateCoords.Count * _eventTileRatio));

            for(int i = 0; i < eventCount && candidateCoords.Count > 0; i++)
            {
                var randomIndex = Random.Range(0, candidateCoords.Count);
                var chosen = candidateCoords[randomIndex];
                _hexMap[chosen].TileType = TileType.Event;
                candidateCoords.RemoveAt(randomIndex);
            }
        }

        private void AssignModeTiles(List<Vector2Int> candidateCoords)
        {
            int totalCount = candidateCoords.Count;

            foreach(var entry in _modeRatio)
            {
                string modeName = entry.modeName;
                float ratio = entry.ratio;
                int count = Mathf.RoundToInt(totalCount * ratio);

                for(int i = 0; i < count && candidateCoords.Count > 0; i++)
                {
                    int randomIndex = Random.Range(0, candidateCoords.Count);
                    var chosen = candidateCoords[randomIndex];
                    _hexMap[chosen].TileType = TileType.Mode;
                    _hexMap[chosen].ModeName = modeName;
                    candidateCoords.RemoveAt(randomIndex);
                }
            }
        }

        private void AssignTileRoles()
        {
            List<Vector2Int> candidateCoords = new List<Vector2Int>(_tileCoords);
            _hexMap[_playerCoord].TileType = TileType.Base;
            candidateCoords.Remove(_playerCoord);

            AssignBossTiles(candidateCoords);
            AssignEventTiles(candidateCoords);
            AssignModeTiles(candidateCoords);

            foreach(var hex in _hexMap.Values)
            {
                hex.SetDebugColorByType();
            }
        }
    }
}
