using System.Collections;

using UnityEngine;

using ZL.Unity;

using ZL.Unity.Unimo;

public partial class PlayerManager : IDamageable
{
    [SerializeField]

    //체력 필요 없나?
    private float currentHealth = 300f;

    [SerializeField]
    
    private float maxHP = 300f;

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

    [SerializeField]
    
    private float playerDamage = 5f;

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
    
    //맞았는지?
    private bool isOnHit = false;

    public delegate void onPlayerDead();

    public event onPlayerDead OnPlayerDead;

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

        PlayHitEffect(contact);

        currentHealth -= damage;//데미지 입음

        if (currentHealth <= 0f)
        {
            currentHealth = 0f;

            canMove = false;

            OnPlayerDead?.Invoke();
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
            BlinkRenderer();

            yield return new WaitForSeconds(onHitBlinkTime);
        }
        
        isOnHit = false;

        ActiveRenderer();

        yield break;
    }

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