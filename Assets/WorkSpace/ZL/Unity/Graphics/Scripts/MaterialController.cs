using UnityEngine;

namespace ZL.Unity.GFX
{
    public abstract class MaterialController : MonoBehaviour
    {
        public abstract Material Material { get; }
    }
}