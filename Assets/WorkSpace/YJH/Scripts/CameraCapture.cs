using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

public class CameraCapture : MonoBehaviour
{
    
    [MenuItem("Tools/Capture/Save Camera View")]
    public static void CaptureFromCamera()
    {
        Camera targetCamera = Camera.main; // 

        if (targetCamera == null)
        {
            Debug.LogError("Main Camera가 없습니다. targetCamera를 직접 지정하거나 MainCamera를 설정하세요.");
            return;
        }

        int width = 1920;
        int height = 1080;

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

        // 저장 경로: 프로젝트 루트 / Captured 폴더
        string folderPath = Application.dataPath + "/../Captured";
        Directory.CreateDirectory(folderPath);
        string filePath = Path.Combine(folderPath, "CameraShot.png");

        File.WriteAllBytes(filePath, screenShot.EncodeToPNG());

        Debug.Log($" 카메라 뷰 저장됨: {filePath}");
    }
    [MenuItem("Tools/Capture Multiple Objects in Fixed Camera")]
    static void InitCapture()
    {
        Camera camera = Camera.main;
        if (camera == null)
        {
            Debug.LogError("MainCamera가 필요합니다.");
            return;
        }

        // 프리팹 리스트
        GameObject[] buildingPrefabs = Selection.gameObjects;
        if (buildingPrefabs.Length == 0)
        {
            Debug.LogError("Hierarchy 또는 Project에서 건물 프리팹들을 선택하세요.");
            return;
        }

        // 고정된 위치 리스트 (배치 후 그 자리에서 찍기)
        Vector3[] positions = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(5, 0, 0),
            new Vector3(10, 0, 0),
            new Vector3(15, 0, 0),
            new Vector3(20, 0, 0)
            // 필요 시 더 추가 가능
        };

        // 카메라 고정값 설정
        Vector3 camPos = new Vector3(0, 10, -10);
        Quaternion camRot = Quaternion.Euler(45, 0, 0);
        float orthoSize = 5f;

        camera.transform.position = camPos;
        camera.transform.rotation = camRot;
        camera.orthographic = true;
        camera.orthographicSize = orthoSize;
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.backgroundColor = new Color(0, 0, 0, 0);

        int width = 512;
        int height = 512;

        string savePath = Application.dataPath + "/../Captured/";
        Directory.CreateDirectory(savePath);

        for (int i = 0; i < Mathf.Min(buildingPrefabs.Length, positions.Length); i++)
        {
            GameObject prefab = buildingPrefabs[i];
            Vector3 pos = positions[i];

            GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            instance.transform.position = pos;

            // 캡처
            RenderTexture rt = new RenderTexture(width, height, 24);
            camera.targetTexture = rt;
            camera.Render();

            RenderTexture.active = rt;
            Texture2D tex = new Texture2D(width, height, TextureFormat.RGBA32, false);
            tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            tex.Apply();

            string fileName = savePath + prefab.name + "_area" + i + ".png";
            File.WriteAllBytes(fileName, tex.EncodeToPNG());

            // 정리
            Object.DestroyImmediate(instance);
            RenderTexture.active = null;
            camera.targetTexture = null;
            rt.Release();

            Debug.Log(" 캡처 완료: {fileName}");
        }
    }
}
