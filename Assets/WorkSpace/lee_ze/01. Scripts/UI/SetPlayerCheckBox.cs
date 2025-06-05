using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerCheckBox : MonoBehaviour
{
    [SerializeField]
    private GameObject onBox;

    [SerializeField]
    private GameObject offBox;

    public void SetCheckBox(bool state)
    {
        onBox.SetActive(state);

        offBox.SetActive(!state);
    }
}
