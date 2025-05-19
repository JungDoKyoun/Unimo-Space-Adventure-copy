using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StackSpellScriptableObject", menuName = "ScriptableObject/StackSpell")]
public class StackSpellScriptableObject : ScriptableObject
{
    public int nowStack;// 현재 충전되어 있는 충전량
    public int maxStack;// 최대 충전량
    public float chargeTime;// 충전에 필요한 시간
    //public float chargeTimer;// 
    public int chargeStack;// 충전되는 양
    
    public int useStack;// 사용하는 충전량
    [Header("대쉬 속도 등")]
    public float skillPower;// 대쉬 속도
    [Header("대쉬 시간 등")]
    public float skillTime;// 대쉬 하는 시간
    //public float dashTimer; // 

}
