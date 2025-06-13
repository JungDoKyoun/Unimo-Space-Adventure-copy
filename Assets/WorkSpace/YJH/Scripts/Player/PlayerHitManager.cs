using Photon.Pun;
using System;
using System.Collections;

using UnityEngine;

using ZL.Unity;

using ZL.Unity.Unimo;

public partial class PlayerManager : IDamageable
{
    [SerializeField]

    //ü�� �ʿ� ����?
    private float currentHealth = 300f;

    [SerializeField]
    
    private float maxHP = 300f;

    [SerializeField]

    //�����ð�
    private float onHitTime = 1.0f;

    [SerializeField]

    // �����ð����� �����̴� ����
    private float onHitBlinkTime = 0.1f;

    [SerializeField]
    
    private SkinnedMeshRenderer cartRenderer;

    [SerializeField]
    
    private SkinnedMeshRenderer bodyRenderer;

    [SerializeField]
    
    private SkinnedMeshRenderer faceRenderer;

    [SerializeField]
    
    private static float playerDamage = 5f;

    [SerializeField]
    
    private GameObject hitEffect;

    [SerializeField]

    private Collider mainCollider;

    public float CurrentHealth
    {
        get => currentHealth;

        set
        {
            if (value < 0f)
            {
                currentHealth = 0f;
            }

            else
            {
                currentHealth = value;
            }
        }
    }
    
    //�¾Ҵ���?
    private bool isOnHit = false;

    public delegate void onPlayerDead();

    public event onPlayerDead OnPlayerDead;

    public delegate void onStageClear();
    public static event onStageClear OnStageClear;
    public static event onStageClear OnStageFail;
    public event Action<float> OnPlayerHit;

    private void OnCollisionStay(Collision collision)
    {
        if (isOnHit == true)
        {
            return;
        }

        if (enemyLayerMask.Contains(collision.gameObject.layer) == false)
        {
            return;
        }

        if (collision.gameObject.TryGetComponent<IDamager>(out var damager) == true)
        {
            damager.GiveDamage(this, collision.GetContact(0).point);
        }
    }

    public void TakeDamage(float damage, Vector3 contact)
    {
        isOnHit = true;

        if (PhotonNetwork.IsConnected == true)
        {
            photonView.RPC("PlayHitEffect", RpcTarget.All, contact);
        }
        else
        {
            PlayHitEffect(contact);
        }

        playerStatus.currentHealth -= damage;//������ ����
        OnPlayerHit?.Invoke(playerStatus.currentHealth);

        if (playerStatus.currentHealth <= 0f)
        {
            playerStatus.currentHealth = 0f;

            canMove = false;

            OnPlayerDead?.Invoke();
            OnStageFail?.Invoke();  
            
        }

        else
        {
            StartCoroutine(PlayerBlink());
        }
    }

    public void PlayHitEffect(Vector3 contact)
    {
        Vector3 forward = contact - mainCollider.bounds.center;

        hitEffect.transform.rotation = Quaternion.LookRotation(forward);

        hitEffect.SetActive(true);
    }

    private IEnumerator PlayerBlink()
    {
        int blinkCount = (int)(onHitTime / onHitBlinkTime);
        
        for (int i = 0; i < blinkCount; i++)
        {
            if (PhotonNetwork.IsConnected)
            {
                photonView.RPC("BlinkRenderer", RpcTarget.All);
            }
            else
            {
                BlinkRenderer();
            }

            yield return new WaitForSeconds(onHitBlinkTime);
        }
        
        isOnHit = false;

        ActiveRenderer();

        yield break;
    }

    [PunRPC]
    public void BlinkRenderer()
    {
        if (bodyRenderer.enabled == false)
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