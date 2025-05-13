using UnityObject = UnityEngine.Object;

namespace ZL.Unity
{
    public static partial class UnityObjectExtensions
    {
        public static void Destroy(this UnityObject instance)
        {
            UnityObject.Destroy(instance);
        }

        public static void DontDestroyOnLoad(this UnityObject instance)
        {
            UnityObject.DontDestroyOnLoad(instance);
        }
    }
}