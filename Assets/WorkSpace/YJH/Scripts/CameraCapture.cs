using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

public class CameraCapture : MonoBehaviour
{

    [Header("캡처할 카메라")]
    public Camera targetCamera;

    [Header("건물 리스트")]
    public List<GameObject> sceneBuildings = new List<GameObject>();

    [Header("캡처 해상도")]
    public int width = 512;
    public int height = 512;

    [Header("저장할 공통 파일명")]
    public string baseFileName = "Building";

    [Header("배경 사진 파일명")]
    public string backgroundFileName = "Background";

    [ContextMenu("배경 사진 촬영")]
    public void CaptureBackgroundOnly()
    {
        if (targetCamera == null)
        {
            
            return;
        }

        
        CaptureCameraToFile(backgroundFileName);
    }

    [ContextMenu("건물별 사진 촬영")]
    public void CaptureSceneBuildings()
    {
        if (targetCamera == null)
        {
            return;
        }

        if (sceneBuildings.Count == 0)
        {
            return;
        }

        string folderPath = Application.dataPath + "/../Captured";
        Directory.CreateDirectory(folderPath);

        // 전체 비활성화
        foreach (var building in sceneBuildings)
        {
            if (building != null)
                building.SetActive(false);
        }

        for (int i = 0; i < sceneBuildings.Count; i++)
        {
            var building = sceneBuildings[i];
            if (building == null) continue;

            building.SetActive(true); // 현재 건물만 보이게
            CaptureCameraToFile($"{baseFileName}_{i}_{building.name}");
            building.SetActive(false); // 다시 숨김
        }

        // 전체 다시 활성화
        foreach (var building in sceneBuildings)
        {
            if (building != null)
                building.SetActive(true);
        }
    }

    private void CaptureCameraToFile(string fileName)
    {
        RenderTexture rt = new RenderTexture(width, height, 24);
        targetCamera.targetTexture = rt;

        Texture2D screenShot = new Texture2D(width, height, TextureFormat.RGB24, false);
        targetCamera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        screenShot.Apply();

        targetCamera.targetTexture = null;
        RenderTexture.active = null;
        rt.Release();

        string folderPath = Application.dataPath + "/../Captured";
        string fullPath = Path.Combine(folderPath, $"{fileName}.png");
        File.WriteAllBytes(fullPath, screenShot.EncodeToPNG());

        Debug.Log($"캡처 완료: {fullPath}");
    }
}
