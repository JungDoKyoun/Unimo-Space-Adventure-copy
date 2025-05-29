using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JDG
{
    public enum ResourcesType
    {
        None, IngameCurrency, MetaCurrency, Blueprint
    }

    [CreateAssetMenu(fileName = "ResourceDataSO", menuName = "SO/EventSO/ResourceDataSO")]
    public class ResourceDataSO : ScriptableObject
    {
        public ResourcesType _resourcesType;
        public string _resourcesName;
        public Sprite _resourcesIcon;
        public string _resourcesDesc;
    }
}
