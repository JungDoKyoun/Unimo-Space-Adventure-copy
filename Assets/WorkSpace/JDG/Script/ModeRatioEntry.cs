using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JDG
{
    public enum ModeType
    {
        None ,Explore, Gather
    }

    [System.Serializable]
    public class ModeRatioEntry
    {
        public ModeType _modeType;
        [Range(0f, 1f)] public float _modeRatio;
    }
}
