using UnityEngine;

namespace ZL.Unity.GFX
{
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