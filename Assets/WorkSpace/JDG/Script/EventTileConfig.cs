using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JDG
{
    [System.Serializable]
    public class EventTileConfig
    {
        [Range(0f, 1f)] public float _eventTileRatio;
        public int _eventMinDistance;
        public List<EventTypeEntry> _eventTypes;
    }
}
