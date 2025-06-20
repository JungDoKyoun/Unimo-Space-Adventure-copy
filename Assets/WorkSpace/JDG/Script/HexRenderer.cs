using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JDG;
using ZL.Unity.Tweening;
using Unity.Mathematics;
using TMPro;
using UnityEditor;

namespace JDG
{
    //6°¢ÇüÀ» ÀÌ·ç´Â °¢ ¸éÀÇ ±¸Á¶Ã¼
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

    //½Ã¾ß
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
        private Material _material; //À°°¢ÇüÀÇ ¸ÓÅ×¸®¾ó
        private Mesh _mesh;
        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;
        private List<Face> _faces; //¸éµéÀÇ ¸ñ·Ï
        private TileData _tileData;
        private float _innerSize; //³»ºÎ ¹İÁö¸§
        private float _outerSize; //¹Ù±ù ¹İÁö¸§
        private float _height; //³ôÀÌ

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

            //Debug.Log($"[DEBUG] Å¸ÀÏ: {name}, À§Ä¡: {transform.position}, Mesh Áß½É: {avg}");
        }

        private void DrawFaces()
        {
            _faces = new List<Face>();

            //À°°¢Å¸ÀÏÀÇ À­¸é
            for(int point = 0; point < 6; point++)
            {
                _faces.Add(CreateFace(_innerSize, _outerSize, _height / 2f, _height / 2f, point));
            }

            //¾Æ·§¸é
            for (int point = 0; point < 6; point++)
            {
                _faces.Add(CreateFace(_innerSize, _outerSize, -_height / 2f, -_height / 2f, point, true));
            }

            //¿·¸éÀÇ ¹Ù±ùºÎºĞ
            for (int point = 0; point < 6; point++)
            {
                _faces.Add(CreateFace(_outerSize, _outerSize, _height / 2f, -_height / 2f, point, true));
            }

            //¿·¸éÀÇ ¾ÈÂÊºÎºĞ
            for (int point = 0; point < 6; point++)
            {
                _faces.Add(CreateFace(_innerSize, _innerSize, _height / 2f, -_height / 2f, point));
            }
        }

        //¸¸µç °¢¸éÀ» ÇÕÄ§
        private void CombineFaces()
        {
            List<Vector3> vertices = new List<Vector3>();
            List<int> tris = new List<int>();
            List<Vector2> uvs = new List<Vector2>();

            //¸é¿¡ÀÖ´Â °¢ Ç×¸ñ ºĞ¸®ÇØ¼­ ±¸ºĞ
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

            //°¢ Ç×¸ñ ÇÕÃÄ¼­ ±×·Á³»±â
            _mesh.vertices = vertices.ToArray();
            _mesh.triangles = tris.ToArray();
            _mesh.uv = uvs.ToArray();
            _mesh.RecalculateNormals();
        }

        //¸é¸¸µé±â
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
            float angle_deg = 60 * index + 30;
            float angle_rad = Mathf.Deg2Rad * angle_deg;
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
                    if(_tileData.TileType != TileType.Boss && _tileData.TileType != TileType.Event)
                    {
                        color = Color.black;
                    }
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

        public void CreateOutlineMesh(Material outlineMat, float width, float yOffset)
        {
<<<<<<< HEAD
=======
            if (_outlineObj != null)
                Destroy(_outlineObj);

            _outlineObj = new GameObject("HighlightOutline");
            _outlineObj.transform.SetParent(transform, false);

>>>>>>> parent of 7bac9495 ([feat] ë‚œì´ë„ ë° í”Œë ˆì´ì–´ ì´ë™ ì„¸ë¶„í™” ë° ì´ë™ê°€ëŠ¥ ë²”ìœ„ í•˜ì´ë¼ì´íŠ¸ ì‘ì„±)
            Vector3[] top = new Vector3[6];
            float outSize = _outerSize;
            float offset = _height * 0.5f + yOffset;

            for(int i = 0; i < 6; i++)
            {
                float rad = Mathf.Deg2Rad * (i * 60 + 30);
                top[i] = new Vector3(outSize * Mathf.Cos(rad), offset, outSize * Mathf.Sin(rad));
            }

            for(int i = 0; i < 6; i++)
            {
                int j = (i + 1) % 6;
                Vector3 p0 = top[i];
                Vector3 p1 = top[j];

                Vector3 dir = (p1 - p0).normalized;
                Vector3 outBound = Vector3.Cross(Vector3.up, dir);

                Vector3 q0 = p0 + outBound * width;
                Vector3 q1 = p1 + outBound * width;

                Mesh quad = new Mesh();
                quad.vertices = new[] { p0, p1, q1, q0 };
                quad.triangles = new[] { 0, 1, 2, 2, 3, 0 };
                quad.RecalculateNormals();

                var obj = new GameObject($"outline_{i}");
                obj.transform.SetParent(transform, false);
                obj.AddComponent<MeshFilter>().sharedMesh = quad;

                var render = obj.AddComponent<MeshRenderer>();
                render.material = outlineMat;
                render.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                render.receiveShadows = false;
                render.material.renderQueue = 3100;
            }
        }

        public void CreateHighlight(Material highlightMaterial, float highlightScale)
        {
<<<<<<< HEAD
            
=======
            if (_highlightObj != null)
                Destroy(_highlightObj);

            _highlightObj = new GameObject("HighlightOutline");
            _highlightObj.transform.SetParent(transform, false);

            Vector3[] top = new Vector3[6];
            float size = _outerSize;
            float offset = _height * 0.5f + yOffset;

            for(int i = 0; i < 6; i++)
            {
                float rad = Mathf.Deg2Rad * (i * 60 + 30);
                top[i] = new Vector3(_outerSize * Mathf.Cos(rad), offset, _outerSize * Mathf.Sin(rad));
            }

            for(int i = 0; i < 6; i++)
            {
                int j = (i + 1) % 6;
                Vector3 p0 = top[i];
                Vector3 p1 = top[j];

                Vector3 dir = (p1 - p0).normalized;
                Vector3 outbound = Vector3.Cross(Vector3.up, dir);

                Vector3 q0 = p0 + outbound * highlightScale;
                Vector3 q1 = p1 + outbound * highlightScale;

                Mesh quad = new Mesh();
                quad.vertices = new[] { p0, p1, q1, q0 };
                quad.triangles = new[] { 0, 1, 2, 2, 3, 0 };
                quad.RecalculateNormals();

                var obj = new GameObject($"highlight_{i}");
                obj.transform.SetParent(_highlightObj.transform, false);

                obj.AddComponent<MeshFilter>().sharedMesh = quad;

                var ren = obj.AddComponent<MeshRenderer>();
                ren.material = highlightMaterial;
                ren.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                ren.receiveShadows = false;
                ren.material.renderQueue = 3100;
            }
            _highlightObj.SetActive(false);
<<<<<<< HEAD
>>>>>>> parent of 7bac9495 ([feat] ë‚œì´ë„ ë° í”Œë ˆì´ì–´ ì´ë™ ì„¸ë¶„í™” ë° ì´ë™ê°€ëŠ¥ ë²”ìœ„ í•˜ì´ë¼ì´íŠ¸ ì‘ì„±)
=======
>>>>>>> parent of 7bac9495 ([feat] ë‚œì´ë„ ë° í”Œë ˆì´ì–´ ì´ë™ ì„¸ë¶„í™” ë° ì´ë™ê°€ëŠ¥ ë²”ìœ„ í•˜ì´ë¼ì´íŠ¸ ì‘ì„±)
        }
    }
}
