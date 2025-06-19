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

        saveFolderPath = Path.Combine(Application.dataPath, "WorkSpace/YJH/Capture");
        Directory.CreateDirectory(saveFolderPath);
        Debug.Log($"Save Path: {saveFolderPath}");
        foreach (var f in buildingsToCapture)
        {
            f.SetActive(false);
        }

        foreach (var building in buildingsToCapture)
        {
            building.SetActive(true);
            CaptureBuildingImage(building);
            building.SetActive(false);
        }
        //foreach (var f in buildingsToCapture)
        //{
        //    f.SetActive(true);
        //}

#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }

    void CaptureBuildingImage(GameObject building)
    {
        // 카메라가 해당 건물을 바라보게 설정
        captureCamera.transform.LookAt(building.transform.position);

        // 전체 시야를 RenderTexture로 렌더링
        int fullWidth = 1024;
        int fullHeight = 1024;
        RenderTexture rt = new RenderTexture(fullWidth, fullHeight, 24);
        captureCamera.targetTexture = rt;
        captureCamera.Render();
        RenderTexture.active = rt;

        // 중앙 정사각형 영역 잘라내기
        int cropSize = captureResolution;
        int startX = (fullWidth - cropSize) / 2;
        int startY = (fullHeight - cropSize) / 2;

        Texture2D cropped = new Texture2D(cropSize, cropSize, TextureFormat.RGBA32, false);
        cropped.ReadPixels(new Rect(startX, startY, cropSize, cropSize), 0, 0);
        cropped.Apply();

        // 렌더 텍스처 해제
        captureCamera.targetTexture = null;
        RenderTexture.active = null;
        rt.Release();

        // 파일 저장
        string fileName = building.name + ".png";
        string filePath = Path.Combine(saveFolderPath, fileName);
        File.WriteAllBytes(filePath, cropped.EncodeToPNG());
        Debug.Log($"캡처 저장 완료: {filePath}");
    }
}