using Photon.Pun;

using System;

using System.Collections;

using UnityEngine;

using UnityEngine.Animations;

using ZL.Unity;

using ZL.Unity.Unimo;

public partial class PlayerManager : IDamageable
{
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

    //[SerializeField]
    //
    //private static float playerDamage = 5f;

    [SerializeField]
    
    private GameObject hitVFX;

    [SerializeField]

    private Collider mainCollider;

    //public RelicData tempRelic;
    public static float gainDamage;
    public float CurrentHealth
    {
        get => PlayerStatus.currentHealth;

        set
        {
            if (value < 0f)
            {
                PlayerStatus tempStatus = PlayerStatus.Clone();

                tempStatus.currentHealth = 0;

                PlayerStatus = tempStatus;
            }

            else
            {
                if (PlayerStatus.currentHealth<value)
                {
                    gainDamage -= value - PlayerStatus.currentHealth;
                }
                
                PlayerStatus tempStatus = PlayerStatus.Clone();

                tempStatus.currentHealth = value;

                PlayerStatus = tempStatus;

                //OnHealthChanged?.Invoke(value);
            }
        }
    }
    
    //�¾Ҵ���?
    private bool isOnHit = false;

    public bool IsOnHit
    {
        set => isOnHit = value;
    }

    public static event Action<float> OnHealthChanged = null;

    public event Action OnPlayerDead = null;

    #pragma warning disable

    // ���� ���� , �������� �Ŵ������� ���� ����
    public static event Action OnStageClear = null;

    // ���� ���� , �������� �Ŵ������� ���� ����
    public static event Action OnStageFail = null;

    #pragma warning restore

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
            //Debug.Log("������ ����?�ݸ���");

            damager.GiveDamage(this, collision.GetContact(0).point);
        }
    }

    public void TakeDamage(float damage, Vector3 contact)
    {
        isOnHit = true;

        //Debug.Log("������ ����");
        
        //PlayHitEffect(contact);

        hitVFX.transform.LookAt(contact, Axis.Y);

        hitVFX.SetActive(true);

        CurrentHealth -= damage;//������ ����

        gainDamage += damage;

        //OnHealthChanged?.Invoke(playerStatus.currentHealth);

        if (PlayerStatus.currentHealth <= 0f)
        {
            //Debug.Log("�÷��̾� ���");
            PlayerStatus.currentHealth = 0f;

            canMove = false;

            OnPlayerDead?.Invoke();

            OnStageFail?.Invoke();
        }

        else
        {
            //Debug.Log("�÷��̾� ������ ���� �۵�");

            //StartCoroutine(PlayerBlink());

            StartPlayerBlink();
        }
    }

    //public void PlayHitEffect(Vector3 contact)
    //{
    //    Vector3 forward = contact - mainCollider.bounds.center;

    //    hitVFX.transform.rotation = Quaternion.LookRotation(forward);

    //    hitVFX.SetActive(true);
    //}

    public void StartPlayerBlink()
    {
        StopPlayerBlink();

        playerBlink = PlayerBlink();

        StartCoroutine(playerBlink);
    }

    public void StopPlayerBlink()
    {
        if (playerBlink == null)
        {
            return;
        }

        StopCoroutine(playerBlink);

        playerBlink = null;
    }

    private IEnumerator playerBlink = null;

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

        playerBlink = null;
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