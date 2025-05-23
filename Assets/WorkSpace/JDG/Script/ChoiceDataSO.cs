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
        public ResourceCostSO _eventRequiredCurrency;
    }
}
