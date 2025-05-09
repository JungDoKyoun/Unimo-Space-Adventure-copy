using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerManager
{
    [SerializeField] float playerHP;//체력 필요 없나?
    bool isOnHit = false;//맞았는지?
    [SerializeField] float onHitTime = 1f;//무적시간
    [SerializeField] float onHitBlinkTime = 0.5f;// 무적시간동안 깜빡이는 간격
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
