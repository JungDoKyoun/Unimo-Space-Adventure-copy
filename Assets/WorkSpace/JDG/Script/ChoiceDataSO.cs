using System.Collections.Generic;
using UnityEngine;
using JDG;

namespace JDG
{
    [CreateAssetMenu(fileName = "ChoiceDataSO", menuName = "SO/EventSO/ChoiceDataSO")]
    public class ChoiceDataSO : ScriptableObject
    {
        public string _choiceName;
        public string _choiceDesc;
        public List<string> _eventRequiredRelics;
        public List<string> _eventRequiredBuildings;
        public ResourceCost _eventRequiredCurrency;
        public List<ProbabilisticEffect> _probabilisticEffect;
    }

    [System.Serializable]
    public class ProbabilisticEffect
    {
        [Range(0f, 1f)] public float _probability;
        public List<EventEffect> _effects;   
    }
}
