using UnityEngine;

using UnityEngine.Android;

using UnityEngine.Events;

using ZL.Unity.Singleton;

namespace ZL.Unity.Android
{
    [AddComponentMenu("ZL/Android/Android Permission Manager (Singleton)")]

    [DefaultExecutionOrder(-1)]

    public sealed class AndroidPermissionManager : MonoSingleton<AndroidPermissionManager>
    {
        [Space]

        [SerializeField]

        private UnityEvent<string> onPermissionGrantedEvent;

        public UnityEvent<string> OnPermissionGrantedEvent
        {
            get => onPermissionGrantedEvent;
        }

        [Space]

        [SerializeField]

        private UnityEvent<string> onPermissionDeniedEvent;

        public UnityEvent<string> OnPermissionDeniedEvent
        {
            get => onPermissionDeniedEvent;
        }

        public void Request(string permissionName)
        {
            if (Permission.HasUserAuthorizedPermission(permissionName) == true)
            {
                OnPermissionGranted(permissionName);

                return;
            }

            var permissionCallbacks = new PermissionCallbacks();

            permissionCallbacks.PermissionGranted += OnPermissionGranted;

            permissionCallbacks.PermissionDenied += OnPermissionDenied;

            Permission.RequestUserPermission(permissionName, permissionCallbacks);
        }

        public void OnPermissionGranted(string permissionName)
        {
            onPermissionGrantedEvent.Invoke(permissionName);
        }

        private void OnPermissionDenied(string permissionName)
        {
            onPermissionDeniedEvent.Invoke(permissionName);
        }
    }
}