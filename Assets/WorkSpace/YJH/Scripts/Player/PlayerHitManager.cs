using Photon.Pun;

using System;

using System.Collections;

using UnityEngine;

using ZL.Unity;

using ZL.Unity.Unimo;

public partial class PlayerManager : IDamageable
{
    [SerializeField]

    //무적시간
    private float onHitTime = 1.0f;

    [SerializeField]

    // 무적시간동안 깜빡이는 간격
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
    
    private GameObject hitEffect;

    [SerializeField]

    private Collider mainCollider;

    //public RelicData tempRelic;
    public static float gainDemage;
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
                    gainDemage -= value - PlayerStatus.currentHealth;
                }
                
                PlayerStatus tempStatus = PlayerStatus.Clone();

                tempStatus.currentHealth = value;

                PlayerStatus = tempStatus;
                //OnHealthChanged?.Invoke(value);
            }
        }
    }
    
    //맞았는지?
    private bool isOnHit = false;

    public bool IsOnHit
    {
        set => isOnHit = value;
    }

    public static event Action<float> OnHealthChanged = null;

    public event Action OnPlayerDead = null;

    #pragma warning disable

    // 삭제 예정 , 스테이지 매니저에서 관리 예정
    public static event Action OnStageClear = null;

    // 삭제 예정 , 스테이지 매니저에서 관리 예정
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
            damager.GiveDamage(this, collision.GetContact(0).point);
            //Debug.Log("데미지 받음?콜리젼");
        }
    }

    public void TakeDamage(float damage, Vector3 contact)
    {
        isOnHit = true;
        //Debug.Log("데미지 받음");
        
        PlayHitEffect(contact);
        

        CurrentHealth -= damage;//데미지 입음
        gainDemage += damage;
        //OnHealthChanged?.Invoke(playerStatus.currentHealth);

        if (PlayerStatus.currentHealth <= 0f)
        {
            //Debug.Log("플레이어 사망");
            PlayerStatus.currentHealth = 0f;

            canMove = false;

            OnPlayerDead?.Invoke();

            OnStageFail?.Invoke();
        }

        else
        {
            //Debug.Log("플레이어 데미지 로직 작동");
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