using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JDG
{
    //6������ �̷�� �� ���� ����ü
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

    //�þ�
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
        private Material _material; //�������� ���׸���
        private Mesh _mesh;
        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;
        private List<Face> _faces; //����� ���
        private TileData _tileData;
        private float _innerSize; //���� ������
        private float _outerSize; //�ٱ� ������
        private float _height; //����

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
        }

        private void DrawFaces()
        {
            _faces = new List<Face>();

            //����Ÿ���� ����
            for(int point = 0; point < 6; point++)
            {
                _faces.Add(CreateFace(_innerSize, _outerSize, _height / 2f, _height / 2f, point));
            }

            //�Ʒ���
            for (int point = 0; point < 6; point++)
            {
                _faces.Add(CreateFace(_innerSize, _outerSize, -_height / 2f, -_height / 2f, point, true));
            }

            //������ �ٱ��κ�
            for (int point = 0; point < 6; point++)
            {
                _faces.Add(CreateFace(_outerSize, _outerSize, _height / 2f, -_height / 2f, point, true));
            }


            //������ ���ʺκ�
            for (int point = 0; point < 6; point++)
            {
                _faces.Add(CreateFace(_innerSize, _innerSize, _height / 2f, -_height / 2f, point));
            }
        }

        //���� ������ ��ħ
        private void CombineFaces()
        {
            List<Vector3> vertices = new List<Vector3>();
            List<int> tris = new List<int>();
            List<Vector2> uvs = new List<Vector2>();

            //�鿡�ִ� �� �׸� �и��ؼ� ����
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

            //�� �׸� ���ļ� �׷�����
            _mesh.vertices = vertices.ToArray();
            _mesh.triangles = tris.ToArray();
            _mesh.uv = uvs.ToArray();
            _mesh.RecalculateNormals();
        }

        //�鸸���
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
                    if (_tileData.ModeName == "Explore")
                        color = Color.cyan;
                    else if (_tileData.ModeName == "Gather")
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
                color = Color.gray;
            }

            _meshRenderer.material.SetColor("_BaseColor", color);
        }

        public void SetTileData(TileData data)
        {
            _tileData = data;
            SetDebugColorByType();
        }
    }
}
