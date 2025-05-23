using System.Collections.Generic;
using UnityEngine;
using JDG;

namespace JDG
{
    public enum EventType
    {
        None, Shop, script
    }

    [System.Serializable]
    public class EventTypeEntry
    {
        public EventType _eventType;
        [Range(0, 1)] public float _ratio;
        public List<EventDataSO> _eventData;
    }
}
