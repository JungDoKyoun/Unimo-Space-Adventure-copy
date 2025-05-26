using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JDG
{
    [CreateAssetMenu(fileName = "ResourceCostSO", menuName = "SO/EventSO/ResourceCost")]
    public class ResourceCostSO : ScriptableObject
    {
        public ResourcesType _resourceType;
        public int _value;
        public Sprite _resourceicon;
    }
}