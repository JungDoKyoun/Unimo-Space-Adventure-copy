using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountSceneUIMgr : MonoBehaviour
{
    [SerializeField]
    private GameObject accountCanvas;

    [SerializeField]
    private GameObject modeSelectionCanvas;

    private void Start()
    {
        SetCanvas();
    }

    private void SetCanvas()
    {
        bool set = FirebaseAuthMgr.HasUser;

        if (set == false)
        {
            accountCanvas.SetActive(!set);

            modeSelectionCanvas.SetActive(set);
        }
        else
        {
            accountCanvas.SetActive(set);

            modeSelectionCanvas.SetActive(!set);
        }
    }
}
