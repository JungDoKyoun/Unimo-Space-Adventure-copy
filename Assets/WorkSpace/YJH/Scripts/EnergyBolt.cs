using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBolt : MonoBehaviour,IAttackType
{
    [SerializeField] float speed=5;
    [SerializeField] float maxRange=5;
    [SerializeField] int energyCost=1;
    [SerializeField] float damage;
    [SerializeField] bool canPierce=false;
    [SerializeField] Transform firePos;
    [SerializeField] GameObject hitEffect;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioSource hitSoundSource;
    [SerializeField] Rigidbody selfBody;
    [SerializeField] Collider selfCollider;
    private Vector3 initialFirePos;




    
    public float Speed
    {
        get { return speed; } set { speed = value; }
    }
    public float MaxRange { get { return maxRange; } set { maxRange = value; } }
    public int EnergyCost { get { return energyCost; } set { energyCost = value; } }
    public float Damage { get { return damage; } set { damage = value; } }
    public bool CanPierce { get { return canPierce; }set { canPierce = value; } }
    public Transform FirePos { get { return firePos; }set { firePos = value; } }
    public GameObject HitEffect { get { return hitEffect; }set { hitEffect = value; } }
    public AudioClip HitSound { get { return hitSound; } set { hitSound = value; } }
    
    public void Shoot(Vector3 fireDirection)
    {
        //transform.LookAt(fireDirection);
        Vector3 tempVelocity = new Vector3(fireDirection.x, 0, fireDirection.z); //fireDirection.normalized * speed;
        firePos = transform;
        selfBody.velocity = tempVelocity.normalized*speed;
        //Debug.Log(selfBody.velocity);
        Destroy(gameObject, maxRange / speed);
        //Debug.Log(transform.forward*speed);
        //Debug.Log(selfBody.velocity);
    }
    private void OnDestroy()
    {
        
    }
    public void OnHit()
    {

        PlayHit();
        Destroy(gameObject);
        //hit 이펙트 활성화

    }
    public void PlayHit()
    {
        if (hitSoundSource != null)
        {
            hitSoundSource.clip = hitSound;
            hitSoundSource.Play();
        }
        Debug.Log("playhit!");
    }

    private void OnTriggerEnter(Collider other)
    {
        selfCollider.enabled = false;
        Debug.Log("trigger");
        OnHit();
    }


}
