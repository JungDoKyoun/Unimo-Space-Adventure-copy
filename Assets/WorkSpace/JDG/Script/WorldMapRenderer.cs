using UnityEngine;

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
            Debug.Log("핸들레이 들어옴1111");
            Vector2 uv = hit.textureCoord;
            float texWidth = _renderTexture.width;
            float texheight = _renderTexture.height;

            float px = uv.x * texWidth;
            float py = uv.y * texheight;

            Vector3 screenPos = new Vector3(px, py, 0);
            //Vector3 screenPos = new Vector3(px, py, _wolrdCam.nearClipPlane + 0.01f);
            //Vector3 worldPos = _wolrdCam.ScreenToWorldPoint(screenPos);

            Ray ray = _wolrdCam.ScreenPointToRay(screenPos);
            if (Physics.Raycast(ray, out RaycastHit worldHit, 100f))
            {
                Vector3 worldPos = worldHit.point;

                Debug.Log($"[WorldPos] 클릭된 위치: {worldPos}");

                Vector2Int coord = _hexGridLayout.GetCoordinateFromPosition(worldPos);
                Debug.Log($"[Coord] 계산된 좌표: {coord}");

                if (_hexGridLayout.TryGetTile(coord, out HexRenderer tile))
                {
                    if (tile.TileData.TileVisibility == TileVisibility.Visible)
                    {
                        Debug.Log("[UI] ShowUI 호출!");
                        _tileSelectionUI.ShowUI(tile, transform.position);
                    }
                }
                else
                {
                    Debug.LogWarning($"[Coord] {coord}에 타일 없음");
                }
            }
            else
            {
                Debug.LogWarning("Raycast 실패함: 아무것도 안 맞음");
            }

            //Vector2Int coord = _hexGridLayout.GetCoordinateFromPosition(worldPos);
            //    if (_hexGridLayout.HexMap.ContainsKey(coord))
            //    {
            //        Debug.Log($"[DEBUG] Coord 존재함: {coord}");
            //    }
            //    else
            //    {
            //        Debug.LogWarning($"[DEBUG] Coord {coord} 에 해당하는 타일 없음");
            //    }

            //    if (_hexGridLayout.TryGetTile(coord, out HexRenderer tile))
            //    {
            //        Debug.Log("핸들레이 들어옴2222");
            //        if (tile.TileData.TileVisibility == TileVisibility.Visible)
            //        {
            //            Debug.Log("핸들레이 들어옴");
            //            _tileSelectionUI.ShowUI(tile, transform.position);
            //        }
            //    }
        }
    }
}
