using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStringHolder : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] string buildingID;
    public string BuildingID {  get { return buildingID; } }
}
