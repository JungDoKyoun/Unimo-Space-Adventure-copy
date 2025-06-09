using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PowerType
{
    None,
    Skill,
    Attack

}
public class CombatBuildBase : ConstructBase
{
    

    public void SetPlayerPower(Object powerType)
    {
        switch (powerType)
        {
            
                case ISpellType:
                SetPlayerSkill (powerType as ISpellType);
                break;
                case GameObject:
                SetPlayerAttack ( powerType as GameObject );
                break;
                default:
                break;

        }
    }
    private void SetPlayerSkill(ISpellType spell)
    {
        if(spell ==null)
        {
            return;
        }
        PlayerManager.SetSpellType (spell);
    }
    private void SetPlayerAttack(GameObject attackPrefab)
    {
        if (attackPrefab == null)
        {
            return;
        }
        PlayerManager.SetAttackType(attackPrefab);
    }

}
