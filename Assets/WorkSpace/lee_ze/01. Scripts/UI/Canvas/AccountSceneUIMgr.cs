using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountSceneUIMgr : MonoBehaviour
{
    [SerializeField]
    private GameObject accountCanvas;

    [SerializeField]
    private GameObject modeSelectionCanvas;

    private Button startButton;

    private void Start()
    {
        SetCanvas(FirebaseAuthMgr.HasUser);

        startButton = accountCanvas.transform.Find("Account/Start Button")?.GetComponent<Button>();

        startButton.onClick.AddListener(() => SetCanvas(FirebaseAuthMgr.HasUser));
    }

    private void SetCanvas(bool hasUser)
    {
        accountCanvas.SetActive(!hasUser);

        modeSelectionCanvas.SetActive(hasUser);
    }

    private void OnDestroy()
    {
        startButton.onClick.RemoveListener(() => SetCanvas(FirebaseAuthMgr.HasUser));
    }
}
