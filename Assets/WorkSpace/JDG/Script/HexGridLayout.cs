using System.Collections.Generic;
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
        [SerializeField] private Vector3 _mapOrigin = Vector3.zero;

        [Header("타일 세팅")]
        [SerializeField] private int _spawnCount = 80;
        [SerializeField] private float _outerSize = 1f;
        [SerializeField] private float _innerSize = 0f;
        [SerializeField] private float _height = 1f;
        [SerializeField] private Material _material;

        [Header("육각타일의 테두리 관련")]
        [SerializeField] private Material _outlineMaterial;
        [SerializeField] private float _outlineExpand;
        [SerializeField] private float _oYOffset;

        [Header("육각타일의 하이라이트 관련")]
        [SerializeField] private Material _highlightMaterial;
        [SerializeField] private float _highlightScale;
        [SerializeField] private float _hYOffset;

        [Header("타일 역할 배치 변수")]
        [SerializeField] private List<ModeRatioEntry> _modeRatio = new List<ModeRatioEntry>();
        [SerializeField] private int[] _bossDistance;
        [SerializeField] private int[] _bossMinGapByDistance;
        [SerializeField] private EventTileConfig _eventTileConfig;


        [Header("플레이어 관련")]
        [SerializeField] private VRPlayerInput _vRPlayerInput;
        private GameObject _playerPrefab;
        private GameObject _playerInstance;

        [Header("UI 관련")]
        private TileSelectionUI _tileSelectionUI;

        [Header("난이도 관련")]
        [SerializeField] private List<DifficultyEntry> _difficultyEntries = new List<DifficultyEntry>();

        [Header("환경 타일 관련")]
        [SerializeField] private int _minEnvironmentTileCount;
        [SerializeField] private int _maxEnvironmentTileCount;
        [SerializeField] private Vector3 _offset;
        private Dictionary<string, GameObject> _environmentPrefabs = new Dictionary<string, GameObject>();
        private Dictionary<Vector2Int, GameObject> _envInstances = new Dictionary<Vector2Int, GameObject>();
        Dictionary<MeshRenderer, Color> _originalColors = new Dictionary<MeshRenderer, Color>();

        private List<Vector2Int> _tileCoords = new List<Vector2Int>(); //타일 좌표 리스트
        private Vector2Int _baseCoord;
        private Vector2Int _playerCoord;
        private Dictionary<Vector2Int, HexRenderer> _hexMap = new Dictionary<Vector2Int, HexRenderer>(); //타일 오브젝트 정보
        private List<Vector2Int> _bossNearShopCount = new List<Vector2Int>();

        private void Start()
        {
            if (GameStateManager.IsRestoreMap && GameStateManager.IsClear)
            {
                SceneLoader.Instance.ClearTile();
                SceneLoader.Instance.ReturnToWorldMap();
                return;
            }

            else if (!GameStateManager.IsRestoreMap && !GameStateManager.IsClear)
            {
                CalculateMapOrigin();
                GenerateConnectedMap();
                LayoutGrid();
                return;
            }
        }

        public Dictionary<Vector2Int, HexRenderer> HexMap { get { return _hexMap; } }
        public Vector2Int PlayerCoord { get { return _playerCoord; } }

        private void GenerateConnectedMap()
        {
            Vector2Int center = new Vector2Int(_gridSize.x / 2, _gridSize.y / 2);
            _tileCoords.Clear();
            _tileCoords.Add(center);
            _playerCoord = center;
            _baseCoord = center;

            List<Vector2Int> frontier = new List<Vector2Int>(GetNeighbors(center));

            while (_tileCoords.Count <= _spawnCount && frontier.Count > 0)
            {
                Vector2Int current = frontier[Random.Range(0, frontier.Count)];
                frontier.Remove(current);

                if (_tileCoords.Contains(current))
                {
                    continue;
                }

                _tileCoords.Add(current);

                foreach (var neighbor in GetNeighbors(current))
                {
                    if (!frontier.Contains(neighbor) && !_tileCoords.Contains(neighbor))
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
                new(x + 1, y), new(x + 1, y + 1), new(x, y + 1), new(x - 1, y), new (x, y -1), new(x + 1, y - 1)
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

            foreach (var coord in _tileCoords)
            {
                GameObject tile = new GameObject($"Hex {coord.x},{coord.y}", typeof(HexRenderer));
                tile.transform.position = GetPositionForHexFromCoordinate(coord);

                HexRenderer hexRenderer = tile.GetComponent<HexRenderer>();
                hexRenderer.OuterSize = _outerSize;
                hexRenderer.InnerSize = _innerSize;
                hexRenderer.Height = _height;
                hexRenderer.SetMaterial(_material);

                var data = new TileData(coord, TileType.None, TileVisibility.Hidden, TileEnvironmentManager.Instance.GetRandomEnvironment(), false, DifficultyType.Easy);
                hexRenderer.SetTileData(data);

                hexRenderer.DrawMesh();
                MeshCollider collider = tile.AddComponent<MeshCollider>();
                collider.sharedMesh = tile.GetComponent<MeshFilter>().mesh;
                _hexMap.Add(coord, hexRenderer);
                tile.transform.SetParent(transform, true);

                //타일 구분선
                hexRenderer.CreateOutlineMesh(_outlineMaterial, _outlineExpand, _oYOffset);

                //하이라이트
                hexRenderer.CreateHighlight(_highlightMaterial, _highlightScale, _hYOffset);
            }

            Vector3 spawnPos = GetPositionForHexFromCoordinate(_playerCoord) + Vector3.up * 1f;
            _playerPrefab = Resources.Load<GameObject>("WorldMap/Player");
            _playerInstance = Instantiate(_playerPrefab, spawnPos, Quaternion.identity);

            var player = _playerInstance.GetComponent<PlayerController>();
            player.Init(this);
            _tileSelectionUI = UIManager.Instance.TileSelectionUI;

            if (_vRPlayerInput != null)
            {
                _vRPlayerInput.Init(player, this);
            }

            if (_tileSelectionUI != null)
            {
                _tileSelectionUI.Init(player, this, _eventTileConfig);
            }

            SceneLoader.Instance.Init(this, player);

            AssignTileRoles();
            AssignEnvironment();
            SetEnvironmentPrefab();
            player.UpdateFog();
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

            return new Vector3(xPosition, 0, -yPosition) + _mapOrigin;
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

        public void UpdateFog(int viewRange)
        {

            foreach (var pair in _hexMap)
            {
                Vector2Int coord = pair.Key;
                HexRenderer hex = pair.Value;

                var dis = HexDistance(coord, _playerCoord);

                if (dis <= viewRange)
                {
                    hex.SetVisibility(TileVisibility.Visible);
                }
                else if (hex.TileData.TileVisibility == TileVisibility.Visible)
                {
                    hex.SetVisibility(TileVisibility.Visited);
                }
            }

            UpdateEnvironmentColorsByVisibility();
        }

        public Vector2Int GetCoordinateFromPosition(Vector3 pos)
        {
            pos -= _mapOrigin;

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
            return _hexMap.TryGetValue(coord, out hex);
        }

        private void AssignBossTiles(List<Vector2Int> candidateCoords)
        {
            List<Vector2Int> placedBosses = new List<Vector2Int>();

            for (int i = 0; i < _bossDistance.Length; i++)
            {
                if (i >= _bossDistance.Length || i >= _bossMinGapByDistance.Length)
                    break;

                int distance = _bossDistance[i];
                int minGap = _bossMinGapByDistance[i];

                List<Vector2Int> valid = candidateCoords.FindAll(coord =>
                HexDistance(_playerCoord, coord) == distance &&
                !placedBosses.Exists(b => HexDistance(b, coord) < minGap));

                if (valid.Count > 0)
                {
                    int randomIndex = Random.Range(0, valid.Count);
                    var coord = valid[randomIndex];
                    _hexMap[coord].TileData.TileType = TileType.Boss;
                    _hexMap[coord].TileData.SceneName = "Boss Stage 1";
                    //난이도 추가되면 위에 씬네임 코드 빼고 이거 넣으면됨
                    //int dis = HexDistance(_baseCoord, coord);
                    //DifficultyType difficultyType = GetDifficultyTypeByDistance(dis);
                    //_hexMap[coord].TileData.DifficultyType = difficultyType;
                    //_hexMap[coord].TileData.SceneName = $"BossScene_{difficultyType}";

                    placedBosses.Add(coord);
                    candidateCoords.Remove(coord);
                    AssignNearbyShopTile(coord, candidateCoords);
                }

                else
                {
                    List<Vector2Int> fallback = new List<Vector2Int>(candidateCoords);
                    fallback.Sort((a, b) => HexDistance(_playerCoord, b).CompareTo(HexDistance(_playerCoord, a)));

                    foreach (var coord in fallback)
                    {
                        if (!placedBosses.Exists(b => HexDistance(b, coord) < minGap))
                        {
                            _hexMap[coord].TileData.TileType = TileType.Boss;
                            _hexMap[coord].TileData.SceneName = "BossScene";
                            //난이도 추가되면 위에 씬네임 코드 빼고 이거 넣으면됨
                            //int dis = HexDistance(_baseCoord, coord);
                            //DifficultyType difficultyType = GetDifficultyTypeByDistance(dis);
                            //_hexMap[coord].TileData.DifficultyType = difficultyType;
                            //_hexMap[coord].TileData.SceneName = $"BossScene_{difficultyType}";

                            placedBosses.Add(coord);
                            candidateCoords.Remove(coord);
                            AssignNearbyShopTile(coord, candidateCoords);
                            break;
                        }
                    }
                }
            }
        }

        private void AssignEventTiles(List<Vector2Int> candidateCoords)
        {
            int eventCount = Mathf.RoundToInt(candidateCoords.Count * _eventTileConfig._eventTileRatio);
            List<Vector2Int> selectedCoords = new List<Vector2Int>();
            int temp = 0;

            while (selectedCoords.Count < eventCount && candidateCoords.Count > 0 && temp < 500)
            {
                var randomIndex = Random.Range(0, candidateCoords.Count);
                var chosen = candidateCoords[randomIndex];
                bool tooClose = selectedCoords.Exists(coord => HexDistance(coord, chosen) < _eventTileConfig._eventMinDistance);
                bool tooClose2 = _bossNearShopCount.Exists(coord => HexDistance(coord, chosen) < _eventTileConfig._eventMinDistance);

                if (tooClose || tooClose2)
                {
                    temp++;
                    continue;
                }

                _hexMap[chosen].TileData.TileType = TileType.Event;
                candidateCoords.RemoveAt(randomIndex);
                selectedCoords.Add(chosen);
            }

            Utiles.Shuffle(selectedCoords);

            int index = 0;
            int total = selectedCoords.Count;

            foreach (var entry in _eventTileConfig._eventTypes)
            {
                int count = Mathf.RoundToInt(selectedCoords.Count * entry._ratio);
                count = Mathf.Min(count, total - index);
                for (int i = 0; i < count; i++)
                {
                    var coord = selectedCoords[index];
                    _hexMap[coord].TileData.EventType = entry._eventType;
                    index++;
                }
            }
        }

        private void AssignModeTiles(List<Vector2Int> candidateCoords)
        {
            int totalCount = candidateCoords.Count;

            foreach (var entry in _modeRatio)
            {
                ModeType modeType = entry._modeType;
                float ratio = entry._modeRatio;
                int count = Mathf.RoundToInt(totalCount * ratio);

                for (int i = 0; i < count && candidateCoords.Count > 0; i++)
                {
                    int randomIndex = Random.Range(0, candidateCoords.Count);
                    var coord = candidateCoords[randomIndex];
                    _hexMap[coord].TileData.TileType = TileType.Mode;
                    _hexMap[coord].TileData.ModeType = modeType;
                    if (modeType == ModeType.Explore)
                    {
                        _hexMap[coord].TileData.SceneName = "Explore Stage Scene";
                    }
                    else if (modeType == ModeType.Gather)
                    {
                        _hexMap[coord].TileData.SceneName = "Gather Stage 1";
                    }
                    //난이도 추가되면 위에 씬네임 코드 빼고 이거 넣으면됨
                    //int dis = HexDistance(_baseCoord, coord);
                    //DifficultyType difficultyType = GetDifficultyTypeByDistance(dis);
                    //_hexMap[coord].TileData.DifficultyType = difficultyType;

                    //if (modeType == ModeType.Explore)
                    //{
                    //    _hexMap[coord].TileData.SceneName = $"ExploreScene_{difficultyType}";
                    //}
                    //else if (modeType == ModeType.Gather)
                    //{
                    //    _hexMap[coord].TileData.SceneName = $"GatherScene_{difficultyType}";
                    //}

                    candidateCoords.RemoveAt(randomIndex);
                }
            }

            if (candidateCoords.Count > 0)
            {
                for (int i = 0; i < candidateCoords.Count; i++)
                {
                    int random = Random.Range(0, _modeRatio.Count);
                    if (random == 0)
                    {
                        var coord = candidateCoords[i];
                        _hexMap[coord].TileData.TileType = TileType.Mode;
                        _hexMap[coord].TileData.ModeType = ModeType.Explore;
                    }
                    else if (random == 1)
                    {
                        var coord = candidateCoords[i];
                        _hexMap[coord].TileData.TileType = TileType.Mode;
                        _hexMap[coord].TileData.ModeType = ModeType.Gather;
                    }
                }
            }
        }

        private void AssignTileRoles()
        {
            List<Vector2Int> candidateCoords = new List<Vector2Int>(_tileCoords);
            _hexMap[_playerCoord].TileData.TileType = TileType.Base;
            candidateCoords.Remove(_playerCoord);

            AssignBossTiles(candidateCoords);
            AssignEventTiles(candidateCoords);
            AssignModeTiles(candidateCoords);

            foreach (var hex in _hexMap.Values)
            {
                hex.SetDebugColorByType();
            }
        }

        public Vector2Int GetBaseCoord()
        {
            return _baseCoord;
        }

        public void RestoreMapState(Dictionary<Vector2Int, TileData> mapData, Vector2Int playerCoord)
        {
            _tileCoords = new List<Vector2Int>(mapData.Keys);
            _hexMap.Clear();

            foreach (var mapD in mapData)
            {
                Vector2Int coord = mapD.Key;
                TileData data = mapD.Value;

                GameObject tile = new GameObject($"Hex {coord.x},{coord.y}", typeof(HexRenderer));
                tile.transform.position = GetPositionForHexFromCoordinate(coord);

                HexRenderer hexRenderer = tile.GetComponent<HexRenderer>();
                hexRenderer.OuterSize = _outerSize;
                hexRenderer.InnerSize = _innerSize;
                hexRenderer.Height = _height;
                hexRenderer.SetMaterial(_material);
                hexRenderer.SetTileData(data);

                hexRenderer.DrawMesh();
                MeshCollider collider = tile.AddComponent<MeshCollider>();
                collider.sharedMesh = tile.GetComponent<MeshFilter>().mesh;
                _hexMap.Add(coord, hexRenderer);
                tile.transform.SetParent(transform, true);

                //타일 구분선
                hexRenderer.CreateOutlineMesh(_outlineMaterial, _outlineExpand, _oYOffset);

                //하이라이트
                hexRenderer.CreateHighlight(_highlightMaterial, _highlightScale, _hYOffset);
            }

            _playerCoord = playerCoord;
            Vector3 spawnPos = GetPositionForHexFromCoordinate(_playerCoord) + Vector3.up * 1f;
            _playerPrefab = Resources.Load<GameObject>("WorldMap/Player");
            _playerInstance = Instantiate(_playerPrefab, spawnPos, Quaternion.identity);

            var player = _playerInstance.GetComponent<PlayerController>();
            player.Init(this);
            _tileSelectionUI = UIManager.Instance.TileSelectionUI;

            if (_vRPlayerInput != null)
            {
                _vRPlayerInput.Init(player, this);
            }

            if (_tileSelectionUI != null)
            {
                _tileSelectionUI.Init(player, this, _eventTileConfig);
            }

            SceneLoader.Instance.Init(this, player);

            SetEnvironmentPrefab();
            player.UpdateFog();
        }

        public void CalculateMapOrigin()
        {
            Vector2Int centerCoord = new Vector2Int(_gridSize.x / 2, _gridSize.y / 2);
            Vector3 centerPos = GetPositionForHexFromCoordinate(centerCoord);
            _mapOrigin = -centerPos;
            transform.position = _mapOrigin;
        }

        private DifficultyType GetDifficultyTypeByDistance(int distance)
        {
            DifficultyType difficultyType = DifficultyType.Easy;

            foreach (var entry in _difficultyEntries)
            {
                if (distance >= entry._distance)
                {
                    difficultyType = entry._difficultyType;
                }
            }
            return difficultyType;
        }

        private void AssignNearbyShopTile(Vector2Int bossCoord, List<Vector2Int> candidateCoords)
        {
            List<Vector2Int> neighbors = new List<Vector2Int>();
            neighbors = GetNeighbors(bossCoord);
            neighbors.Sort((a, b) => HexDistance(_baseCoord, a).CompareTo(HexDistance(_baseCoord, b)));

            foreach (var coord in neighbors)
            {
                if (candidateCoords.Contains(coord))
                {
                    _hexMap[coord].TileData.TileType = TileType.Event;
                    _hexMap[coord].TileData.EventType = EventType.Shop;
                    _bossNearShopCount.Add(coord);
                    candidateCoords.Remove(coord);
                    break;
                }
            }
        }

        //private void AssignEnvironment()
        //{
        //    List<Vector2Int> availableCoords = new List<Vector2Int>();

        //    foreach (var coord in _tileCoords)
        //    {
        //        TileType type = _hexMap[coord].TileData.TileType;

        //        if (type != TileType.Event && type != TileType.Boss)
        //        {
        //            availableCoords.Add(coord);
        //        }
        //        else
        //        {
        //            _hexMap[coord].TileData.EnvironmentType = EnvironmentType.None;
        //        }
        //    }

        //    availableCoords.Sort((a, b) => a.x != b.x ? a.x.CompareTo(b.x) : a.y.CompareTo(b.y));

        //    List<EnvironmentType> allEvT = TileEnvironmentManager.Instance.GetAllEnvironmentTypes().FindAll(evt => evt != EnvironmentType.None);
        //    Dictionary<EnvironmentType, int> allEnvironmentTypeCount = new Dictionary<EnvironmentType, int>();

        //    foreach (var type in allEvT)
        //    {
        //        allEnvironmentTypeCount[type] = 0;
        //    }

        //    int totalCount = availableCoords.Count;
        //    int eventTypeMaxCount = Mathf.CeilToInt(totalCount / allEvT.Count);
        //    int index = 0;

        //    while (index < availableCoords.Count)
        //    {
        //        int remain = availableCoords.Count - index;
        //        int size = (remain < _minEnvironmentTileCount) ? remain : Random.Range(_minEnvironmentTileCount, Mathf.Min(_maxEnvironmentTileCount + 1, remain + 1));
        //        EnvironmentType evT = GetBalancedEnviromentType(allEvT, allEnvironmentTypeCount, eventTypeMaxCount);

        //        for (int i = 0; i < size && index < availableCoords.Count; i++)
        //        {
        //            Vector2Int coord = availableCoords[index];
        //            _hexMap[coord].TileData.EnvironmentType = evT;
        //            index++;
        //        }
        //    }
        //}

        //private EnvironmentType GetBalancedEnviromentType(List<EnvironmentType> types, Dictionary<EnvironmentType, int> envCount, int eventMaxCount)
        //{
        //    List<EnvironmentType> candidate = new List<EnvironmentType>();

        //    foreach (var type in types)
        //    {
        //        if (envCount[type] < eventMaxCount)
        //        {
        //            candidate.Add(type);
        //        }
        //    }

        //    if (candidate.Count == 0)
        //    {
        //        return types[Random.Range(0, types.Count)];
        //    }

        //    EnvironmentType chose = candidate[Random.Range(0, candidate.Count)];
        //    envCount[chose]++;
        //    return chose;
        //}

        private void AssignEnvironment()
        {
            List<Vector2Int> availableCoords = new List<Vector2Int>();

            foreach (var coord in _tileCoords)
            {
                TileType tileType = _hexMap[coord].TileData.TileType;

                if (tileType != TileType.Event && tileType != TileType.Boss && tileType != TileType.Base)
                {
                    availableCoords.Add(coord);
                }
                else
                {
                    _hexMap[coord].TileData.EnvironmentType = EnvironmentType.None;
                }
            }

            availableCoords.Sort((a, b) => a.y != b.y ? a.y.CompareTo(b.y) : a.x.CompareTo(b.x));

            List<EnvironmentType> allEvn = TileEnvironmentManager.Instance.GetAllEnvironmentTypes().FindAll(type => type != EnvironmentType.None);
            Dictionary<EnvironmentType, int> envTypeCount = new Dictionary<EnvironmentType, int>();

            foreach (var type in allEvn)
            {
                envTypeCount[type] = 0;
            }

            int total = availableCoords.Count;
            int maxEnvCount = Mathf.CeilToInt(total / allEvn.Count);
            HashSet<Vector2Int> used = new HashSet<Vector2Int>();

            foreach (var start in availableCoords)
            {
                EnvironmentType chosenEnv = GetBalancedEnviromentType(allEvn, envTypeCount, maxEnvCount);
                int size = Random.Range(_minEnvironmentTileCount, _maxEnvironmentTileCount + 1);
                List<Vector2Int> choseCoord = GetEnvironmentCluster(start, availableCoords, used, size);

                foreach (var coord in choseCoord)
                {
                    _hexMap[coord].TileData.EnvironmentType = chosenEnv;
                    used.Add(coord);
                }

                if (used.Count >= total)
                    break;
            }
        }

        private EnvironmentType GetBalancedEnviromentType(List<EnvironmentType> environmentTypes, Dictionary<EnvironmentType, int> envTypeCount, int maxEnvCount)
        {
            List<EnvironmentType> candidate = new List<EnvironmentType>();

            foreach (var type in environmentTypes)
            {
                if (envTypeCount[type] < maxEnvCount)
                {
                    candidate.Add(type);
                }
            }

            if (candidate.Count == 0)
            {
                return environmentTypes[Random.Range(0, environmentTypes.Count)];
            }

            EnvironmentType chose = candidate[Random.Range(0, candidate.Count)];
            envTypeCount[chose]++;
            return chose;
        }

        private List<Vector2Int> GetEnvironmentCluster(Vector2Int start, List<Vector2Int> availableCoords, HashSet<Vector2Int> used, int size)
        {
            List<Vector2Int> result = new List<Vector2Int>();
            Queue<(Vector2Int coord, int dist)> queue = new Queue<(Vector2Int coord, int dist)>();
            HashSet<Vector2Int> visited = new HashSet<Vector2Int>();

            queue.Enqueue((start, 0));
            visited.Add(start);

            while (queue.Count > 0 && result.Count < size)
            {
                var (coord, dist) = queue.Dequeue();

                if (!availableCoords.Contains(coord) || used.Contains(coord))
                    continue;

                result.Add(coord);

                foreach (var neighbor in GetNeighbors(coord))
                {
                    if (availableCoords.Contains(neighbor) && !used.Contains(neighbor) && !visited.Contains(neighbor))
                    {
                        queue.Enqueue((neighbor, dist + 1));
                        visited.Add(neighbor);
                    }
                }
            }

            return result;
        }

        private void SetEnvironmentPrefab()
        {
            foreach (var coord in _tileCoords)
            {
                TileData data = _hexMap[coord].TileData;
                GameObject environmentPrefab = null;

                switch (data.EnvironmentType)
                {
                    case (EnvironmentType.None):
                        {
                            if (data.TileType == TileType.Boss)
                            {
                                environmentPrefab = GetEnvironmentPrefab("WorldMap/Environment/Boss");
                            }
                            else if (data.TileType == TileType.Event)
                            {
                                environmentPrefab = GetEnvironmentPrefab("WorldMap/Environment/Event");
                            }
                            break;
                        }

                    case (EnvironmentType.Dark):
                        environmentPrefab = GetEnvironmentPrefab("WorldMap/Environment/Dark");
                        break;

                    case (EnvironmentType.Volcano):
                        environmentPrefab = GetEnvironmentPrefab("WorldMap/Environment/Volcano");
                        break;

                    case (EnvironmentType.Ice):
                        environmentPrefab = GetEnvironmentPrefab("WorldMap/Environment/Ice");
                        break;
                }

                if (environmentPrefab == null)
                    continue;

                Vector3 tilePos = GetPositionForHexFromCoordinate(data.Coord);
                tilePos += _offset;
                Quaternion rotation = Quaternion.Euler(0, 90, 0);
                GameObject instance = Instantiate(environmentPrefab, tilePos, rotation);
                instance.transform.SetParent(_hexMap[coord].transform);
                instance.SetActive(false);
                _envInstances.Add(coord, instance);
            }
        }

        private GameObject GetEnvironmentPrefab(string path)
        {
            if (_environmentPrefabs.TryGetValue(path, out var prefab))
                return prefab;

            prefab = Resources.Load<GameObject>(path);

            if (prefab != null)
                _environmentPrefabs[path] = prefab;

            return prefab;
        }
        public void UpdateEnvironmentColorsByVisibility()
        {
            foreach(var dic in _envInstances)
            {
                Vector2Int coord = dic.Key;
                GameObject obj = dic.Value;

                if (!_hexMap.ContainsKey(coord))
                    continue;

                TileData tileData = _hexMap[coord].TileData;
                var renders = obj.GetComponentsInChildren<MeshRenderer>();

                foreach(var render in renders)
                {
                    if (!render.material.HasProperty("_BaseColor"))
                        continue;

                    if (!_originalColors.ContainsKey(render))
                    {
                        _originalColors[render] = render.material.GetColor("_BaseColor");
                    }

                    Color originalColor = _originalColors[render];

                    switch (tileData.TileVisibility)
                    {
                        case TileVisibility.Visible:
                            render.material.SetColor("_BaseColor", originalColor);
                            break;

                        case TileVisibility.Visited:
                            render.material.SetColor("_BaseColor", originalColor * 0.4f);
                            break;

                        case TileVisibility.Hidden:
                            if (tileData.TileType == TileType.Boss || tileData.TileType == TileType.Event)
                            {
                                obj.SetActive(true);
                            }
                            else
                            {
                                obj.SetActive(false);
                            }
                            break;
                    }
                }

                if(tileData.TileVisibility != TileVisibility.Hidden)
                {
                    obj.SetActive(true);
                }
            }
        }
    }
}
