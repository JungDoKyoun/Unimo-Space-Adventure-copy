using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerManager
{
    [SerializeField] float playerHP;//체력 필요 없나?
    bool isOnHit = false;//맞았는지?
    [SerializeField] float onHitTime = 1f;//무적시간
    [SerializeField] float onHitBlinkTime = 0.5f;// 무적시간동안 깜빡이는 간격
    [SerializeField] SkinnedMeshRenderer cartRenderer;
    [SerializeField] SkinnedMeshRenderer bodyRenderer;
    [SerializeField] SkinnedMeshRenderer faceRenderer;
    [SerializeField] float playerDamage=5f;
    [SerializeField] GameObject hitEffect;

    public delegate void onPlayerDead();
    public event onPlayerDead OnPlayerDead;
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
        if (isOnHit == false)// 피격중이 아닐때 데미지 함수 호출 방식에 따라서 이 문항 지워야 할 수도 있음 
        {
            
            playerHP -= dmg;//데미지 입음
            PlayHitEffect();
            if (playerHP < 0)
            {
                playerHP = 0;
                OnPlayerDead?.Invoke();
            }
            else
            {
                StartCoroutine(PlayerBlink());
            }
        }
        else
        {
            return;//피격중일 때
        }
    }
    public void PlayHitEffect()
    {
        hitEffect.SetActive(true);
    }
    IEnumerator PlayerBlink()
    {
        BlinkRenderer();
        yield return new WaitForSeconds(onHitBlinkTime);
        BlinkRenderer();
        yield return new WaitForSeconds(onHitBlinkTime);
        BlinkRenderer();
        isOnHit = false;
        yield break;
    }
    public void BlinkRenderer()
    {
        if(bodyRenderer.enabled == false)
        {
            bodyRenderer.enabled = true;
        }
        else
        {
            bodyRenderer.enabled = false;
        }

        if (faceRenderer.enabled == false)
        {
            faceRenderer.enabled = true;
        }
        else
        {
            faceRenderer.enabled = false;
        }

        if (cartRenderer.enabled == false)
        {
            cartRenderer.enabled = true;
        }
        else
        {
            cartRenderer.enabled = false;
        }
    }
    
    



}
