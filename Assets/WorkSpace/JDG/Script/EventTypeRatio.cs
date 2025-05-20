using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JDG
{
    public enum EventType
    {
        None, Shop
    }

    [System.Serializable]
    public class EventTypeRatio
    {
        public EventType _eventType;
        [Range(0, 1)] public float _ratio;
    }
}
