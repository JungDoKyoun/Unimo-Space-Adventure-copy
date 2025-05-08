using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JDG
{
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

    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class HexRenderer : MonoBehaviour
    {
        [SerializeField] Material _material;
        private Mesh _mesh;
        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;
        private List<Face> _faces;
        [SerializeField] float _innerSize;
        [SerializeField] float _outerSize;
        [SerializeField] float _height;

        private void Awake()
        {
            _mesh = new Mesh();
            _meshFilter = GetComponent<MeshFilter>();
            _meshRenderer = GetComponent<MeshRenderer>();

            _meshFilter.mesh = _mesh;
            if (_material != null)
                _meshRenderer.material = _material;
        }

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

            for(int point = 0; point < 6; point++)
            {
                _faces.Add(CreateFace(_innerSize, _outerSize, _height / 2f, _height / 2f, point));
            }

            for (int point = 0; point < 6; point++)
            {
                _faces.Add(CreateFace(_innerSize, _outerSize, -_height / 2f, -_height / 2f, point, true));
            }

            for (int point = 0; point < 6; point++)
            {
                _faces.Add(CreateFace(_outerSize, _outerSize, _height / 2f, -_height / 2f, point, true));
            }

            for (int point = 0; point < 6; point++)
            {
                _faces.Add(CreateFace(_innerSize, _innerSize, _height / 2f, -_height / 2f, point));
            }
        }

        private void CombineFaces()
        {
            List<Vector3> vertices = new List<Vector3>();
            List<int> tris = new List<int>();
            List<Vector2> uvs = new List<Vector2>();

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

            _mesh.vertices = vertices.ToArray();
            _mesh.triangles = tris.ToArray();
            _mesh.uv = uvs.ToArray();
            _mesh.RecalculateNormals();
        }

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

        protected Vector3 GetPoint(float size, float height, int index)
        {
            float angle_deg = 60 * index;
            float angle_rad = Mathf.PI / 180f * angle_deg;
            return new Vector3((size * Mathf.Cos(angle_rad)), height, size * Mathf.Sin(angle_rad));
        }

        public void SetMaterial(Material material)
        {
            _material = material;
            if (_meshRenderer != null)
                _meshRenderer.material = _material;
        }
    }
}
