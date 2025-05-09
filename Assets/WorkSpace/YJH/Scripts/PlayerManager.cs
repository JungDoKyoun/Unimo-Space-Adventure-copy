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
}
