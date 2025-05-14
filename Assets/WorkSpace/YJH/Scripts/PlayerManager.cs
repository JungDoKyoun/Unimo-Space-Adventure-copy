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
        float tempdmg = 1f;
        if (collision.gameObject.layer==enemyLayerMask && isOnHit == false)//������ �ǰݽ� 
        {
            isOnHit = true;
            PlayerGetDemage(tempdmg);


        }
    }

}
