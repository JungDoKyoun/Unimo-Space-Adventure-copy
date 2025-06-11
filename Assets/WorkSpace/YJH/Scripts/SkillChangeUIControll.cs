using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkillChangeUIControll : MonoBehaviour
{
    [SerializeField] List<Button> changePlayerSkillButtons=new List<Button>();
   

    private void Start()
    {
        changePlayerSkillButtons[0].onClick.AddListener(() => ChangePlayerSkill(new Dash()));
        changePlayerSkillButtons[1].onClick.AddListener(() => ChangePlayerSkill(new tempskill1()));//테스트1
        changePlayerSkillButtons[2].onClick.AddListener(() => ChangePlayerSkill(new tempskill2 ()));//테스트2
    }
   

    private void ChangePlayerSkill(ISpellType spell)
    {
        PlayerManager.SetSpellType(spell);
        
    }



    public void TempNext()
    {
        SceneManager.LoadScene("TestScene");
    }

}
