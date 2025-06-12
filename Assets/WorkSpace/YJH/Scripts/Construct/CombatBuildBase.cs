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

    public void SetPlayerPower()// ������ �� �� ����? , �������̽��� ������?, ���� ��ų�� ���� ��ũ��Ʈ ���̴� �׷�
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