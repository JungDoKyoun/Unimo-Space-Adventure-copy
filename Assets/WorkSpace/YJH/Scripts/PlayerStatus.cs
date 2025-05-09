using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerManager
{
    [SerializeField] float playerHP;//ü�� �ʿ� ����?
    bool isOnHit = false;//�¾Ҵ���?
    [SerializeField] float onHitTime = 1f;//�����ð�
    [SerializeField] float onHitBlinkTime = 0.5f;// �����ð����� �����̴� ����
    public float PlayerHP
    {
        get {  return playerHP; }
        set 
        {
            if (value < 0)
            {
                playerHP = 0;
            }
            else
            {
                playerHP = value;
            }
        }
    }
    public void PlayerGetDemage(float dmg)
    {
        playerHP -= dmg;
        if(playerHP < 0)
        {
            playerHP = 0;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }



}
