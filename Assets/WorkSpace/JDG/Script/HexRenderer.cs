using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JDG;
using ZL.Unity.Tweening;

namespace JDG
{
    //6각형을 이루는 각 면의 구조체
    public struct Face
    {
        public List<Vector3> Vertices { get; private set; }
        public List<int> Triangles { get; private set; }
        public List<Vector2> UVS { get; private set; }

        public Face(List<Vector3> vertices, List<int> triangles, List<Vector2> uvs)
        {
            Vertices = vertices;
            Triangles = triangles;
            UVS = uvs;
        }
    }

    //시야
    public enum TileVisibility
    {
        Hidden,
        Visible,
        Visited
    }

    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class HexRenderer : MonoBehaviour
    {
        private Material _material; //육각형의 머테리얼
        private Mesh _mesh;
        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;
        private List<Face> _faces; //면들의 목록
        private TileData _tileData;
        private float _innerSize; //내부 반지름
        private float _outerSize; //바깥 반지름
        private float _height; //높이

        private void Awake()
        {
            _mesh = new Mesh();
            _meshFilter = GetComponent<MeshFilter>();
            _meshRenderer = GetComponent<MeshRenderer>();

            _meshFilter.mesh = _mesh;
            if (_material != null)
                _meshRenderer.material = _material;
        }

        public TileData TileData { get { return _tileData; } }
        public float InnerSize { get { return _innerSize; } set { _innerSize = value; } }
        public float OuterSize { get { return _outerSize; } set { _outerSize = value; } }
        public float Height { get { return _height; } set { _height = value; } }

        private void OnEnable()
        {
            DrawMesh();
        }

        private void OnValidate()
        {
            if (_mesh == null || _meshFilter == null || _meshRenderer == null) return;
            if (Application.isPlaying)
            {
                DrawMesh();
            }
        }

        public void DrawMesh()
        {
            DrawFaces();
            CombineFaces();

            Vector3 avg = Vector3.zero;
            foreach (var v in _mesh.vertices)
                avg += v;

            avg /= _mesh.vertexCount;

            Debug.Log($"[DEBUG] 타일: {name}, 위치: {transform.position}, Mesh 중심: {avg}");
        }

        private void DrawFaces()
        {
            _faces = new List<Face>();

            //육각타일의 윗면
            for(int point = 0; point < 6; point++)
            {
                _faces.Add(CreateFace(_innerSize, _outerSize, _height / 2f, _height / 2f, point));
            }

            //아랫면
            for (int point = 0; point < 6; point++)
            {
                _faces.Add(CreateFace(_innerSize, _outerSize, -_height / 2f, -_height / 2f, point, true));
            }

            //옆면의 바깥부분
            for (int point = 0; point < 6; point++)
            {
                _faces.Add(CreateFace(_outerSize, _outerSize, _height / 2f, -_height / 2f, point, true));
            }

            //옆면의 안쪽부분
            for (int point = 0; point < 6; point++)
            {
                _faces.Add(CreateFace(_innerSize, _innerSize, _height / 2f, -_height / 2f, point));
            }
        }

        //만든 각면을 합침
        private void CombineFaces()
        {
            List<Vector3> vertices = new List<Vector3>();
            List<int> tris = new List<int>();
            List<Vector2> uvs = new List<Vector2>();

            //면에있는 각 항목 분리해서 구분
            for(int i = 0; i < _faces.Count; i++)
            {
                vertices.AddRange(_faces[i].Vertices);
                uvs.AddRange(_faces[i].UVS);

                int offset = (4 * i);

                foreach(int triangle in _faces[i].Triangles)
                {
                    tris.Add(triangle + offset);
                }
            }

            //각 항목 합쳐서 그려내기
            _mesh.vertices = vertices.ToArray();
            _mesh.triangles = tris.ToArray();
            _mesh.uv = uvs.ToArray();
            _mesh.RecalculateNormals();
        }

        //면만들기
        private Face CreateFace(float innerRad, float outerRad, float heightA, float heightB, int point, bool reverse = false)
        {
            Vector3 pointA = GetPoint(innerRad, heightB, point);
            Vector3 pointB = GetPoint(innerRad, heightB, (point < 5) ? point + 1 : 0);
            Vector3 pointC = GetPoint(outerRad, heightA, (point < 5) ? point + 1 : 0);
            Vector3 pointD = GetPoint(outerRad, heightA, point);

            List<Vector3> vertices = new List<Vector3>() { pointA, pointB, pointC, pointD };
            List<int> triangels = new List<int>() { 0, 1, 2, 2, 3, 0 };
            List<Vector2> uvs = new List<Vector2>() { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) };

            if(reverse)
            {
                vertices.Reverse();
            }

            return new Face(vertices, triangels, uvs);
        }

        private Vector3 GetPoint(float size, float height, int index)
        {
            float angle_deg = 60 * index - 30;
            float angle_rad = Mathf.PI / 180f * angle_deg;
            return new Vector3((size * Mathf.Cos(angle_rad)), height, size * Mathf.Sin(angle_rad));
        }

        public void SetMaterial(Material material)
        {
            _material = new Material(material);
            if (_meshRenderer != null)
                _meshRenderer.material = _material;
        }

        public void SetVisibility(TileVisibility visibility)
        {
            _tileData.TileVisibility = visibility;
            SetDebugColorByType();
        }

        public TileVisibility GetTileVisibility()
        {
            return _tileData.TileVisibility;
        }

        public void SetDebugColorByType()
        {
            if (_meshRenderer == null || _meshRenderer.material == null)
                return;

            Color color = Color.white;

            switch(_tileData.TileType)
            {
                case TileType.Base:
                    color = Color.blue;
                    break;
                case TileType.Boss:
                    color = Color.red;
                    break;
                case TileType.Event:
                    color = Color.yellow;
                    break;
                case TileType.Mode:
                    if (_tileData.ModeType == ModeType.Explore)
                        color = Color.cyan;
                    else if (_tileData.ModeType == ModeType.Gather)
                        color = Color.green;
                    else
                        color = Color.magenta;
                    break;
                case TileType.None:
                default:
                    color = Color.white;
                    break;
            }

            switch (_tileData.TileVisibility)
            {
                case TileVisibility.Hidden:
                    color = Color.black;
                    break;
                case TileVisibility.Visited: 
                    color *= 0.5f; 
                    break;
                case TileVisibility.Visible: 
                    break;
            }

            if(_tileData.IsCleared)
            {
                if (_tileData.TileType == TileType.Base)
                    return;

                color = Color.gray;
            }

            _meshRenderer.material.SetColor("_BaseColor", color);
        }

        public void SetTileData(TileData data)
        {
            _tileData = data;
            SetDebugColorByType();
        }

        public void CreateOutlineMesh(Material outlineMat, float outlineExpand)
        {
            GameObject outlineObj = new GameObject("Outline");
            outlineObj.transform.SetParent(this.transform, false);
            outlineObj.transform.localPosition = Vector3.zero;
            outlineObj.transform.localRotation = Quaternion.identity;
            outlineObj.transform.localScale = Vector3.one;

            MeshFilter filter = outlineObj.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = outlineObj.AddComponent<MeshRenderer>();

            Mesh outlineMesh = Instantiate(_mesh);

            Vector3[] vertices = outlineMesh.vertices;
            Vector3[] normals = outlineMesh.normals;

            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] += new Vector3(vertices[i].x, 0, vertices[i].z).normalized * outlineExpand;
            }

            outlineMesh.vertices = vertices;
            outlineMesh.normals = normals;
            outlineMesh.RecalculateBounds();

            filter.mesh = outlineMesh;
            meshRenderer.material = outlineMat;

            meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            meshRenderer.receiveShadows = false;
            meshRenderer.material.renderQueue = 2999;
        }
    }
}
