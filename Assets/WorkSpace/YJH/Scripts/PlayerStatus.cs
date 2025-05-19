using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

using ZL.Unity.Unimo;

public partial class PlayerManager : IDamageable
{
    [SerializeField] float maxHP = 300;
    [SerializeField] float currentHealth;//ü�� �ʿ� ����?
    
    bool isOnHit = false;//�¾Ҵ���?
    [SerializeField] float onHitTime = 1.0f;//�����ð�
    [SerializeField] float onHitBlinkTime = 0.1f;// �����ð����� �����̴� ����
    [SerializeField] SkinnedMeshRenderer cartRenderer;
    [SerializeField] SkinnedMeshRenderer bodyRenderer;
    [SerializeField] SkinnedMeshRenderer faceRenderer;
    [SerializeField] int playerDamage = 5;
    [SerializeField] GameObject hitEffect;

    public delegate void onPlayerDead();
    public event onPlayerDead OnPlayerDead;
    public float CurrentHealth
    {
        get {  return currentHealth; }
        set 
        {
            if (value < 0)
            {
                currentHealth = 0;
            }
            else
            {
                currentHealth = value;
            }
        }
    }
    public void StatusStart()
    {
        currentHealth = maxHP;
    }
    public void TakeDamage(int damage)
    {
        isOnHit = true;

        currentHealth -= damage;//������ ����

         PlayHitEffect();

        if (currentHealth <= 0)
        {
            Debug.Log("dead");
            currentHealth = 0;
            OnPlayerDead?.Invoke();
        }
        else
        {
            Debug.Log("startblink");
            StartCoroutine(PlayerBlink());
        }
    }

    public void PlayHitEffect()
    {
        hitEffect.SetActive(true);
    }
    IEnumerator PlayerBlink()
    {
        int blinkCount = (int)(onHitTime / onHitBlinkTime);
        
        for (int i = 0; i < blinkCount; i++)
        {
            
            BlinkRenderer();
            yield return new WaitForSeconds(onHitBlinkTime);
        }
        
        isOnHit = false;
        ActiveRenderer();
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
    public void ActiveRenderer()
    {
        bodyRenderer.enabled = true;
        faceRenderer.enabled = true;
        cartRenderer.enabled = true;
    }
    



}
