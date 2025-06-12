using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZL.Unity.Pooling;
using ZL.Unity.Unimo;

public class Energy2 : PooledObject, IDamageable, IEnergy
{
    public int energy { get { return 2; } }

    public float CurrentHealth { get; set; }

    public void TakeDamage(float damage, Vector3 contact)
    {
        gameObject.SetActive(false);
    }
}
