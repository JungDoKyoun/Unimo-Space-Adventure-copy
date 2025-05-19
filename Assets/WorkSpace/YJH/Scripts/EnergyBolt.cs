using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZL.Unity.Unimo;

public class EnergyBolt : MonoBehaviour, IAttackType, IDamager
{
    [SerializeField] float speed=5;
    [SerializeField] float maxRange=5;
    [SerializeField] int energyCost=1;
    [SerializeField] float damage;//float으로?
    [SerializeField] bool canPierce=false;
    [SerializeField] Transform firePos;
    [SerializeField] GameObject hitEffect;

    [SerializeField] AudioClip hitSound;//사용 안함
    [SerializeField] AudioSource hitSoundSource;//사용 안함

    [SerializeField] Rigidbody selfBody;
    [SerializeField] Collider selfCollider;
    private Vector3 initialFirePos;




    
    public float Speed
    {
        get { return speed; } set { speed = value; }
    }
    public float MaxRange { get { return maxRange; } set { maxRange = value; } }
    public int EnergyCost { get { return energyCost; } set { energyCost = value; } }
    public float Damage { get { return damage; } set { damage = value; } }//float으로?
    public bool CanPierce { get { return canPierce; }set { canPierce = value; } }
    public Transform FirePos { get { return firePos; }set { firePos = value; } }
    public GameObject HitEffect { get { return hitEffect; }set { hitEffect = value; } }
    public AudioClip HitSound { get { return hitSound; } set { hitSound = value; } }

    private void Start()
    {
        
    }
    public void Shoot(Vector3 fireDirection)
    {
        
        Vector3 tempVelocity = new Vector3(fireDirection.x, 0, fireDirection.z); //fireDirection.normalized * speed;
        firePos = transform;
        selfBody.velocity = tempVelocity.normalized*speed;
        
        Destroy(gameObject, maxRange / speed);
        
    }
    private void OnDestroy()
    {
        var temp=Instantiate(hitEffect,transform.position,Quaternion.identity);
        Destroy(temp, 2f);
        //PlayHit();
    }
    public void OnHit()
    {

        

        Destroy(gameObject);
        //hit 이펙트 활성화

    }
    //public void PlayHit()
    //{
    //    if (hitSoundSource != null)
    //    {
    //        hitSoundSource.clip = hitSound;
    //        hitSoundSource.Play();
    //        hitEffect.SetActive(true);
    //    }
    //   // Debug.Log("playhit!");
    //}

    private void OnTriggerEnter(Collider other)
    {
        selfCollider.enabled = false;
        //Debug.Log("trigger");
        OnHit();
    }

    public void GiveDamage(IDamageable damageable)
    {
        damageable.TakeDamage(damage);
    }
}
