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

    public void SetPlayerPower()// 구조를 좀 더 쉽게? , 인터페이스로 나눌까?, 문제 스킬은 오직 스크립트 뿐이다 그러
    {
        if(attackPrefab == null)
        {
            Debug.Log("noattackprefab");
        }
        else
        {
            SetPlayerAttack(attackPrefab);
        }
        if (spellTypeIndex == 0||spellTypeIndex>=ConstructManager.playerSpells.Length) 
        {
            Debug.Log("can'tsetspell");
        }
        else
        {
            SetPlayerSkill(ConstructManager.playerSpells[spellTypeIndex]);
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