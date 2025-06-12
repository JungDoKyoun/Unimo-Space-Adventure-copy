using UnityEngine;

namespace ZL.Unity.GFX
{
    [DefaultExecutionOrder((int)ScriptExecutionOrder.FastAwake)]

    public abstract class MaterialController : MonoBehaviour
    {
        public abstract Material Material { get; }
    }
}