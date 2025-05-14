using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public partial class PlayerManager : MonoBehaviour
{

    

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
        Debug.Log("collision");
        float tempdmg = 1f;
        Debug.Log(enemyLayerMask);
        Debug.Log(collision.gameObject.layer);
        if (collision.gameObject.layer==LayerMask.NameToLayer("tempenemy") && isOnHit == false)//적에게 피격시 
        {
            
            Debug.Log("playerhit");
            Vector3 hitDir = collision.GetContact(0).normal;

            hitEffect.transform.rotation = Quaternion.FromToRotation(hitDir, transform.rotation.eulerAngles);
            PlayerGetDemage(tempdmg);
            isOnHit = true;


        }
    }

}
