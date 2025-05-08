using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JDG;

namespace JDG
{
    public class HexGridLayout : MonoBehaviour
    {
        [Header("그리드 세팅")]
        [SerializeField] private Vector2Int _gridSize;

        [Header("타이틀 세팅")]
        [SerializeField] float _outerSize = 1f;
        [SerializeField] float _innerSize = 0f;
        [SerializeField] float _height = 1f;
        [SerializeField] Material _material;

        private void OnEnable()
        {
            LayoutGrid();
        }

        private void OnValidate()
        {
            if(Application.isPlaying)
            {
                LayoutGrid();
            }
        }

        private void LayoutGrid()
        {
            for(int y = 0; y < _gridSize.y; y++)
            {
                for(int x = 0; x < _gridSize.x; x++)
                {
                    GameObject tile = new GameObject($"Hex {x},{y}", typeof(HexRenderer));
                    tile.transform.position = GetPositionForHexFromCoordinate(new Vector2Int(x, y));

                    HexRenderer hexRenderer = tile.GetComponent<HexRenderer>();
                    hexRenderer.OuterSize = _outerSize;
                    hexRenderer.InnerSize = _innerSize;
                    hexRenderer.Height = _height;
                    hexRenderer.SetMaterial(_material);
                    hexRenderer.DrawMesh();

                    tile.transform.SetParent(transform, true);
                }
            }
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

            offset = (shouldOffset) ? width / 2 : 0;

            xPosition = (column * horizontalDistance) + offset;
            yPosition = (row * verticalDistance);

            return new Vector3(xPosition, 0, -yPosition);
        }
    }
}
