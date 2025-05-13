using UnityEngine;

namespace ZL.Unity
{
    public static partial class ScreenEx
    {
        public static RenderTexture RenderTexture { get; private set; } = new RenderTexture(Screen.width, Screen.height, 24);

        public static void SetResolution(int width, int height, FullScreenMode fullScreenMode)
        {
            float ratio = (float)width / height;

            float screenRatio = (float)Screen.width / Screen.height;

            Screen.SetResolution(width, (int)((float)Screen.height / Screen.width * width), fullScreenMode);

            if (ratio < screenRatio)
            {
                float newScreenWidth = ratio / screenRatio;

                Camera.main.rect = new Rect((1f - newScreenWidth) / 2f, 0f, newScreenWidth, 1f);
            }

            else
            {
                float newScreenHeight = screenRatio / ratio;

                Camera.main.rect = new Rect(0f, (1f - newScreenHeight) / 2f, 1f, newScreenHeight);
            }

            RenderTexture = new RenderTexture(width, height, 24);
        }

        public static void SetResolution(int width, int height)
        {
            SetResolution(width, height, Screen.fullScreenMode);
        }

        public static Texture2D GetScreenPixels(TextureFormat textureFormat)
        {
            var texture2D = new Texture2D(Screen.width, Screen.height, textureFormat, false);

            texture2D.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height), 0, 0);

            texture2D.Apply();

            return texture2D;
        }
    }
}