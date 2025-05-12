using UnityEngine;

using ZL.Unity.IO;

namespace ZL.Unity
{
    [AddComponentMenu("ZL/Application Manager Receiver")]

    [DisallowMultipleComponent]

    public sealed class ApplicationManagerReceiver : MonoBehaviour
    {
        public BoolPref RunInBackgroundPref
        {
            get => ApplicationManager.Instance.RunInBackgroundPref;
        }

        public IntPref TargetFrameRatePref
        {
            get => ApplicationManager.Instance.TargetFrameRatePref;
        }

        public void Quit()
        {
            ApplicationManager.Instance.Quit();
        }
    }
}