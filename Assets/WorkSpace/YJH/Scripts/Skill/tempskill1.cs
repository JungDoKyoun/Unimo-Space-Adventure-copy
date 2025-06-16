using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempskill1 : ISpellType, IStackSpell
{
    public int NowStack { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public int MaxStack => throw new System.NotImplementedException();

    public float ChargeTime => throw new System.NotImplementedException();

    public int UseStack { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void InitSpell()
    {
        return;
        throw new System.NotImplementedException();
    }

    public bool ReturnState()
    {
        return false;
        throw new System.NotImplementedException();
    }

    public void SetPlayer(PlayerManager player)
    {
        return;
    }

    public void SetState(bool state)
    {
        throw new System.NotImplementedException();
    }

    public void UpdateTime()
    {
        return;
        throw new System.NotImplementedException();
    }

    public void UseSpell()
    {
        Debug.Log("skill1");
        return;
    }

    // Start is called before the first frame update
    
}
