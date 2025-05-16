using UnityEngine;

namespace JDG
{
    public class WorldMapRenderer : MonoBehaviour
    {
        [Header("�ʿ��� ������Ʈ��")]
        [SerializeField] private Camera _wolrdCam;
        [SerializeField] private RenderTexture _renderTexture;
        [SerializeField] private HexGridLayout _hexGridLayout;
        [SerializeField] private TileSelectionUI _tileSelectionUI;

        public void HandleRayHit(RaycastHit hit)
        {
            Debug.Log("�ڵ鷹�� ����1111");
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

                Debug.Log($"[WorldPos] Ŭ���� ��ġ: {worldPos}");

                Vector2Int coord = _hexGridLayout.GetCoordinateFromPosition(worldPos);
                Debug.Log($"[Coord] ���� ��ǥ: {coord}");

                if (_hexGridLayout.TryGetTile(coord, out HexRenderer tile))
                {
                    if (tile.TileData.TileVisibility == TileVisibility.Visible)
                    {
                        Debug.Log("[UI] ShowUI ȣ��!");
                        _tileSelectionUI.ShowUI(tile, transform.position);
                    }
                }
                else
                {
                    Debug.LogWarning($"[Coord] {coord}�� Ÿ�� ����");
                }
            }
            else
            {
                Debug.LogWarning("Raycast ������: �ƹ��͵� �� ����");
            }

            //Vector2Int coord = _hexGridLayout.GetCoordinateFromPosition(worldPos);
            //    if (_hexGridLayout.HexMap.ContainsKey(coord))
            //    {
            //        Debug.Log($"[DEBUG] Coord ������: {coord}");
            //    }
            //    else
            //    {
            //        Debug.LogWarning($"[DEBUG] Coord {coord} �� �ش��ϴ� Ÿ�� ����");
            //    }

            //    if (_hexGridLayout.TryGetTile(coord, out HexRenderer tile))
            //    {
            //        Debug.Log("�ڵ鷹�� ����2222");
            //        if (tile.TileData.TileVisibility == TileVisibility.Visible)
            //        {
            //            Debug.Log("�ڵ鷹�� ����");
            //            _tileSelectionUI.ShowUI(tile, transform.position);
            //        }
            //    }
        }
    }
}
