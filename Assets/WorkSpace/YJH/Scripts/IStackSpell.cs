using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStackSpell
{
    public int NowStack { get; set; }
    public int MaxStack { get; }
    public float ChargeTime { get; }
    public int UseStack { get; set; }
   
}
