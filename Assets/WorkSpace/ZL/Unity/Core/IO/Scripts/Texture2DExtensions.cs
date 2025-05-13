using UnityEngine;

namespace ZL.Unity.IO
{
    public static partial class Texture2DExtensions
    {
        public static byte[] EncodeTo(this Texture2D instance, ImageFileFormat format)
        {
            return format switch
            {
                ImageFileFormat.EXR => instance.EncodeToEXR(),

                ImageFileFormat.JPG => instance.EncodeToJPG(),

                ImageFileFormat.PNG => instance.EncodeToPNG(),

                ImageFileFormat.TGA => instance.EncodeToTGA(),

                _ => null
            };
        }
    }
}