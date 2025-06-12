using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZL.Unity.Unimo;
using ZL.Unity.Pooling;

public class Energy1 : PooledObject, IDamageable, IEnergy
{
    public int energy { get { return 1; } }

    public float CurrentHealth { get; set; }

    public void TakeDamage(float damage, Vector3 contact)
    {
        gameObject.SetActive(false);
    }
}