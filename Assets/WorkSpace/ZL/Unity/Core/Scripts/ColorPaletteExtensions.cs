using UnityEngine;

namespace ZL.Unity
{
    public static partial class ColorPaletteExtensions
    {
        public static Color ToColor(this ColorPalette instance) => instance switch
        {
            ColorPalette.Red => new Color32(255, 0, 0, 255),

            ColorPalette.Green => new Color32(0, 255, 0, 255),

            ColorPalette.Blue => new Color32(0, 0, 255, 255),

            ColorPalette.Black => new Color32(0, 0, 0, 255),

            ColorPalette.White => new Color32(255, 255, 255, 255),

            ColorPalette.Yellow => new Color32(255, 255, 0, 255),

            ColorPalette.Cyan => new Color32(0, 255, 255, 255),

            ColorPalette.Magenta => new Color32(255, 0, 255, 255),

            ColorPalette.Gray => new Color32(128, 128, 128, 255),

            ColorPalette.Clear => new Color32(0, 0, 0, 0),

            _ => default
        };
    }
}