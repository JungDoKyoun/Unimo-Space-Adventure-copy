using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using ZL.Unity;

using ZL.Unity.Unimo;

public partial class PlayerManager 
{
    void OnEnable()
    {
        MonsterManager.Instance.Target = transform;
    }

    void OnDisable()
    {
        if (MonsterManager.Instance != null)
        {
            MonsterManager.Instance.Target = null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ActionStart();
        MoveStart();
    }

    // Update is called once per frame
    void Update()
    {
        ActionUpdate();
        MoveUpdate();
    }
    private void OnTriggerEnter(Collider other)
    {
        
        
        if(other.gameObject.layer==LayerMask.NameToLayer("Energy"))
        {
            GetEnergy(1);
            Destroy(other.gameObject);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        /*Debug.Log("collision");
        int tempdmg = 1;
        Debug.Log(damagerLayerMask);
        Debug.Log(collision.gameObject.layer);
        if (collision.gameObject.layer==LayerMask.NameToLayer("tempenemy") && isOnHit == false)//적에게 피격시 
        {
            
            Debug.Log("playerhit");
            Vector3 hitDir = collision.GetContact(0).normal;

            hitEffect.transform.rotation = Quaternion.FromToRotation(hitDir, transform.rotation.eulerAngles);
            TakeDamage(tempdmg);
            isOnHit = true;
        }*/

        if (isOnHit == true)
        {
            return;
        }

        if (damagerLayerMask.Contains(collision.gameObject.layer) == false)
        {
            return;
        }

        if (collision.gameObject.TryGetComponent<IDamager>(out var damager) == true)
        {
            Vector3 hitDir = collision.GetContact(0).normal;

            hitEffect.transform.rotation = Quaternion.FromToRotation(hitDir, transform.rotation.eulerAngles);

            damager.GiveDamage(this);
        }
    }
}
