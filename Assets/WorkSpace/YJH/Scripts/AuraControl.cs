using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraControl : MonoBehaviour
{

    [SerializeField] GameObject player;
    // Start is called before the first frame update
    
    private void FixedUpdate()
    {
        transform.position = player.transform.position;
    }

}
