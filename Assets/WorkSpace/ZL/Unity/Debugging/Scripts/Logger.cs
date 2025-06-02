#if UNITY_EDITOR

using UnityEngine;

namespace ZL.Unity.Debugging
{
    [AddComponentMenu("ZL/Debugging/Logger")]

    public sealed class Logger : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private string message;

        public string Message
        {
            get => message;

            set => message = value;
        }

        public void Log()
        {
            Log(message);
        }

        public void Log(string message)
        {
            FixedDebug.Log(message);
        }
    }
}

#endif