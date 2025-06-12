using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZL.Unity.Pooling;
using ZL.Unity.Unimo;

public class Energy3 : PooledObject, IDamageable, IEnergy
{
    public int energy { get { return 3; } }

    public float CurrentHealth { get; set; }

    public void TakeDamage(float damage, Vector3 contact)
    {
        gameObject.SetActive(false);
    }
}
