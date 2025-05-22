using UnityEngine;
using JDG;

namespace JDG
{
    public class WorldMapRenderer : MonoBehaviour
    {
        [Header("필요한 컴포넌트들")]
        [SerializeField] private Camera _wolrdCam;
        [SerializeField] private RenderTexture _renderTexture;
        [SerializeField] private HexGridLayout _hexGridLayout;
        [SerializeField] private TileSelectionUI _tileSelectionUI;

        public void HandleRayHit(RaycastHit hit)
        {
            Vector2 uv = hit.textureCoord;
            float texWidth = _renderTexture.width;
            float texheight = _renderTexture.height;

            float px = uv.x * texWidth;
            float py = uv.y * texheight;

            Vector3 screenPos = new Vector3(px, py, 0);


            Ray ray = _wolrdCam.ScreenPointToRay(screenPos);
            if (Physics.Raycast(ray, out RaycastHit worldHit, 100f))
            {
                Vector3 worldPos = worldHit.point;

                Vector2Int coord = _hexGridLayout.GetCoordinateFromPosition(worldPos);

                if (coord == _hexGridLayout.PlayerCoord)
                    return;

                if (_hexGridLayout.TryGetTile(coord, out HexRenderer tile))
                {
                    if (tile.TileData.TileVisibility == TileVisibility.Visible)
                    {
                        _tileSelectionUI.ShowUI(tile, transform.position);
                    }
                }
            } 
        }
    }
}
