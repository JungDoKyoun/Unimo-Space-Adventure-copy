using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ModeRatioEntry 
{
    public string modeName;
    [Range(0f, 1f)] public float ratio;
}
