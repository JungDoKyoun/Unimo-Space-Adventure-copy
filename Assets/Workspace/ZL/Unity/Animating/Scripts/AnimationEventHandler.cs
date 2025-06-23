using UnityEngine;

using UnityEngine.Events;

using ZL.Unity.Collections;

namespace ZL.Unity.Animating
{
    [AddComponentMenu("ZL/Animating/Animation Event Handler")]

    public sealed class AnimationEventHandler : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private SerializableDictionary<string, UnityEvent<AnimationEvent>> events = null;

        public void Invoke(AnimationEvent @event)
        {
            events[@event.stringParameter].Invoke(@event);
        }
    }
}