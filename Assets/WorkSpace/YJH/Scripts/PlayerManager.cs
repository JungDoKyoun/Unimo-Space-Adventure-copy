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
        float tempdmg = 1f;
        if (false && isOnHit == false)//적에게 피격시 
        {
            isOnHit = true;
            PlayerGetDemage(tempdmg);
        }
        else if(other.gameObject.layer==LayerMask.NameToLayer("Energy"))
        {
            GetEnergy(1);
            Destroy(other.gameObject);
        }

    }

}
