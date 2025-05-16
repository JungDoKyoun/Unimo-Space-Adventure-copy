using UnityEngine;

namespace ZL.Unity.GFX
{
    [AddComponentMenu("ZL/GFX/Skybox Rotator")]

    public sealed class SkyboxRotator : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private float speed = 1f;

        private void Update()
        {
            RenderSettings.skybox.SetFloat("_Rotation", speed * Time.time);
        }
    }
}