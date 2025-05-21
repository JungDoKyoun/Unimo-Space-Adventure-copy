using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JDG
{
    [CreateAssetMenu(fileName = "EventDataSO", menuName = "SO/EventSO/EventDataSO")]
    public class EventDataSO : ScriptableObject
    {
        public string _eventID;
        public EventType _eventType;
        public string _eventTitle;
        public string _eventDesc;
        public List<ChoiceData> _eventChoice;
    }

    [System.Serializable]
    public class ChoiceData
    {
        public string _choiceName;
        public string _choiceDesc;
        public List<string> _eventRequiredRelics;
        public List<string> _eventRequiredBuildings;
        public List<ConditionData> _eventRequiredCurrency;
    }

    [System.Serializable]
    public class ConditionData
    {
        public string key;
        public string op; 
        public int value;
    }
}
