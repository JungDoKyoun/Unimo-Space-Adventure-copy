using UnityEngine.UI;

namespace ZL.Unity.UI
{
    public static partial class GraphicExtensions
    {
        public static void SetAnchorStrech<TGraphic>(this TGraphic instance)

            where TGraphic : Graphic
        {
            instance.rectTransform.SetAnchorStrech();
        }
    }
}