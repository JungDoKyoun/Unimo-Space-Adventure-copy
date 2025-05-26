using System.Collections.Generic;
using UnityEngine;
using JDG;

namespace JDG
{
    public enum ResourcesType
    {
        None, Gold
    }

    [CreateAssetMenu(fileName = "EventDataSO", menuName = "SO/EventSO/EventDataSO")]
    public class EventDataSO : ScriptableObject
    {
        public string _eventID;
        public EventType _eventType;
        public string _eventTitle;
        public string _eventDesc;
        public List<ChoiceDataSO> _eventChoices;
        public List<RelicDataSO> _relicDatas;
    }
}
