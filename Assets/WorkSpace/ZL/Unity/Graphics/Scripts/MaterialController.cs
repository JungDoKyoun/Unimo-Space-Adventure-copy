using UnityEngine;

namespace ZL.Unity.GFX
{
    [DefaultExecutionOrder((int)ScriptExecutionOrder.Awake)]

    public abstract class MaterialController : MonoBehaviour
    {
        public abstract Material Material { get; }
    }
}