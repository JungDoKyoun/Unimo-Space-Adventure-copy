using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerManager
{
    [SerializeField] float playerHP;//ü�� �ʿ� ����?
    bool isOnHit = false;//�¾Ҵ���?
    [SerializeField] float onHitTime = 1f;//�����ð�
    [SerializeField] float onHitBlinkTime = 0.5f;// �����ð����� �����̴� ����
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
        if (isOnHit == false)// �ǰ����� �ƴҶ� ������ �Լ� ȣ�� ��Ŀ� ���� �� ���� ������ �� ���� ���� 
        {
            
            playerHP -= dmg;//������ ����
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
            return;//�ǰ����� ��
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
