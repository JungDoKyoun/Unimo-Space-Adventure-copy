using UnityEngine;

namespace ZL.Unity
{
    public sealed class ReadOnlyWhenEditModeAttribute : CustomPropertyAttribute
    {
        #if UNITY_EDITOR

        protected override void Draw(Drawer drawer)
        {
            drawer.IsEnabled = Application.isPlaying;
        }

        #endif
    }
}