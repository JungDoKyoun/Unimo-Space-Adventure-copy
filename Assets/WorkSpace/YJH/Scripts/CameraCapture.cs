using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;


public class CameraCapture : MonoBehaviour
{
    public KeyCode captureKey = KeyCode.Q;
    public int width = 1024;
    public int height = 1024;

    private string saveFolderPath;
    private Camera targetCamera;
    private void Start()
    {
        // 저장 경로를 Assets/WorkSpace/YJH/Capture 로 설정
        saveFolderPath = Path.Combine(Application.dataPath, "WorkSpace/YJH/Capture");
        Directory.CreateDirectory(saveFolderPath);
        targetCamera = Camera.main ?? FindObjectOfType<Camera>();
        if (targetCamera == null)
        {
            
            return;
        }

        // 투명 배경 설정
        targetCamera.clearFlags = CameraClearFlags.SolidColor;
        targetCamera.backgroundColor = new Color(0, 0, 0, 0);

    }

    private void Update()
    {
        if (Input.GetKeyDown(captureKey))
        {
            StartCoroutine(CaptureTransparent());
            Debug.Log("pressed");


        }

    }

    private IEnumerator CaptureTransparent()
    {
        yield return new WaitForEndOfFrame();

        RenderTexture rt = new RenderTexture(width, height, 24, RenderTextureFormat.ARGB32);
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGBA32, false);

        targetCamera.targetTexture = rt;
        RenderTexture.active = rt;

        targetCamera.Render();

        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();

        targetCamera.targetTexture = null;
        RenderTexture.active = null;
        rt.Release();

        byte[] pngBytes = tex.EncodeToPNG();

        string fileName = GenerateFileName();
        string filePath = Path.Combine(saveFolderPath, fileName);
        File.WriteAllBytes(filePath, pngBytes);
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
        //Debug.Log($"📸 투명 배경 캡처 저장 완료: {filePath}");
    }

    private string GenerateFileName()
    {
        int index = 1;
        string fileName;
        do
        {
            fileName = $"building{index}.png";
            index++;
        } while (File.Exists(Path.Combine(saveFolderPath, fileName)));
        return fileName;
    }
}