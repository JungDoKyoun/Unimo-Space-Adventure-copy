using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;


public class CameraCapture : MonoBehaviour
{
    [Header("캡처 카메라 설정")]
    public Camera captureCamera;
    public int captureResolution = 512;

    [Header("대상 건물 리스트")]
    public List<GameObject> buildingsToCapture;

    private string saveFolderPath;

    // 카메라가 건물을 찍을 때 사용할 위치 오프셋 (건물 위치 기준 카메라 상대 위치)
    public Vector3 cameraOffset = new Vector3(0, 10, -10);

    void Start()
    {

        saveFolderPath = Path.Combine(Application.dataPath, "/WorkSpace/YJH/Capture");
        Directory.CreateDirectory(saveFolderPath);

        foreach (var building in buildingsToCapture)
        {
            CaptureBuildingImage(building);
        }

#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }

    void CaptureBuildingImage(GameObject building)
    {
        // 카메라 위치 이동 & 건물 바라보기
        captureCamera.transform.position = building.transform.position + cameraOffset;
        captureCamera.transform.LookAt(building.transform.position);

        // RenderTexture 준비
        RenderTexture rt = new RenderTexture(captureResolution, captureResolution, 24);
        captureCamera.targetTexture = rt;

        // 캡처용 텍스처 생성
        Texture2D screenShot = new Texture2D(captureResolution, captureResolution, TextureFormat.RGBA32, false);

        // 렌더링 & 읽기
        captureCamera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, captureResolution, captureResolution), 0, 0);
        screenShot.Apply();

        // 정리
        captureCamera.targetTexture = null;
        RenderTexture.active = null;
        rt.Release();

        // 저장
        string fileName = building.name + ".png";
        string filePath = Path.Combine(saveFolderPath, fileName);
        File.WriteAllBytes(filePath, screenShot.EncodeToPNG());

        Debug.Log($"캡처 저장 완료: {filePath}");
    }
}