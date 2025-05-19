using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpellType
{
    public void UseSpell();
    public void InitSpell();
    public void UpdateTime();
    public void SetPlayer(PlayerManager player);

    public bool ReturnState();






}
