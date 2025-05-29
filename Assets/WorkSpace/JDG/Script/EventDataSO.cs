using System.Collections.Generic;
using UnityEngine;
using JDG;
using UnityEngine.UI;

namespace JDG
{
    [CreateAssetMenu(fileName = "EventDataSO", menuName = "SO/EventSO/EventDataSO")]
    public class EventDataSO : ScriptableObject
    {
        public string _eventID;
        public EventType _eventType;
        public Sprite _eventImage;
        public string _eventTitle;
        public string _eventDesc;
        [Range(0, 1)] public float _eventWeight;
        public List<ChoiceDataSO> _eventChoices;
        public List<RelicDataSO> _relicDatas;
    }
}
