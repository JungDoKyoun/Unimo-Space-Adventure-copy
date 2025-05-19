using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StackSpellScriptableObject", menuName = "ScriptableObject/StackSpell")]
public class StackSpellScriptableObject : ScriptableObject
{
    public int nowStack;// ���� �����Ǿ� �ִ� ������
    public int maxStack;// �ִ� ������
    public float chargeTime;// ������ �ʿ��� �ð�
    //public float chargeTimer;// 
    public int chargeStack;// �����Ǵ� ��
    
    public int useStack;// ����ϴ� ������
    [Header("�뽬 �ӵ� ��")]
    public float skillPower;// �뽬 �ӵ�
    [Header("�뽬 �ð� ��")]
    public float skillTime;// �뽬 �ϴ� �ð�
    //public float dashTimer; // 

}
