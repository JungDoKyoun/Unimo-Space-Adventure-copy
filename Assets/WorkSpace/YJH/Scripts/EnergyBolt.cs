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
    
    public void Shoot(Vector3 firePosition)
    {
        speed = 5;
        maxRange = 5;
        energyCost = 1;
        damage = 1;
        canPierce = false;
    }




    
    
}
