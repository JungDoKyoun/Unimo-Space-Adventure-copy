using UnityEngine;

using UnityEngine.Events;

using ZL.Unity.Collections;

namespace ZL.Unity
{
    [AddComponentMenu("ZL/Event Hanlder (Vector3)")]

    public sealed class EventHandler_Vector3 : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private Vector3 param = Vector3.zero;

        public Vector3 Param
        {
            get => param;

            set => param = value;
        }

        public float ParamX
        {
            set => param.x = value;
        }

        public float ParamY
        {
            set => param.y = value;
        }

        public float ParamZ
        {
            set => param.z = value;
        }

        [Space]

        [SerializeField]

        private SerializableDictionary<string, UnityEvent<Vector3>> events = null;

        public void Invoke(string key)
        {
            events[key].Invoke(param);
        }
    }
}