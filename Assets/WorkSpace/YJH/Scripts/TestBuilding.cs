using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBuilding : MonoBehaviour
{


    [SerializeField] ConstructBase buildInfo;


    // Start is called before the first frame update
    private void Awake()
    {
        buildInfo = Resources.Load<ConstructBase>("Construct/test");
    }
   
}
