using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackType 
{
    public float Speed
    {
        get; set;
    }
    public float MaxRange
    {
        get; set;
    }
    public int EnergyCost
    {
        get; set;
    }
    public float Damage
    {
        get; set;
    }
    public bool CanPierce
    {
        get; set;
    }
    public Transform FirePos
    {
        get; set;
    }
    public GameObject HitEffect
    {
        get; set;
    }
    public AudioClip HitSound
    {
        get; set;
    }

    public void Shoot(Vector3 firePos);
    
    









}
