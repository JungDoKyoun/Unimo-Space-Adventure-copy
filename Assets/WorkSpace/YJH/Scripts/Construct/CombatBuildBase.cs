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
[CreateAssetMenu(fileName = "CombatBuildBase", menuName = "ScriptableObject/CombatBuilding")]
public class CombatBuildBase : ConstructBase
{
    public GameObject attackPrefab;
    public int spellTypeIndex;

    public void SetPlayerPower()
    {
        if(attackPrefab == null&& spellTypeIndex != 0)
        {
            SetPlayerSkill(ConstructManager.playerSpells[spellTypeIndex]);
        }else if (spellTypeIndex == 0 && attackPrefab != null)
        {
            SetPlayerAttack(attackPrefab);
        }
        else
        {
            return;
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
