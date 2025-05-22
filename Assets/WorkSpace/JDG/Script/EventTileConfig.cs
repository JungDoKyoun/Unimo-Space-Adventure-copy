using System.Collections.Generic;
using UnityEngine;
using JDG;

namespace JDG
{
    [CreateAssetMenu(fileName = "EventTileConfig", menuName = "SO/EventSO/EventTileConfig")]
    public class EventTileConfig : ScriptableObject
    {
        [Range(0f, 1f)] public float _eventTileRatio;
        public int _eventMinDistance;
        public List<EventTypeEntry> _eventTypes;
    }
}

