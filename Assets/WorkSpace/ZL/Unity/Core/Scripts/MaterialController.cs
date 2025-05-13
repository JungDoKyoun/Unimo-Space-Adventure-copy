using UnityEngine;

namespace ZL.Unity
{
    [DisallowMultipleComponent]

    public abstract class MaterialController : MonoBehaviour
    {
        public abstract Material[] Materials { get; }

        public Material Material => Materials[0];

        public Material Get(int index)
        {
            return Materials[index];
        }
    }
}