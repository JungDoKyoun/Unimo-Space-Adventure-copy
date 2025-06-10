using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountSceneUIMgr : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup accountGroup;

    [SerializeField]
    private CanvasGroup modeSelectionGroup;

    private Button startButton;

    private Button logoutButton;

    private void Start()
    {
        SetCanvas(FirebaseAuthMgr.HasUser);

        startButton = accountGroup.transform.Find("Account/Start Button")?.GetComponent<Button>();

        logoutButton = modeSelectionGroup.transform.Find("Logout Button")?.GetComponent<Button>();

        startButton.onClick.AddListener(() => SetCanvas(true));

        logoutButton.onClick.AddListener(() =>
        {
            Debug.Log(FirebaseAuthMgr.HasUser);

            FirebaseAuthMgr.Instance.SetButtonInteractable(FirebaseAuthMgr.HasUser);

            SetCanvas(false);
        });
    }

    private void SetCanvas(bool hasUser)
    {
        SetGroup(accountGroup, !hasUser);

        SetGroup(modeSelectionGroup, hasUser);
    }

    private void SetGroup(CanvasGroup group, bool enable)
    {
        group.alpha = enable ? 1f : 0f;

        group.interactable = enable;

        group.blocksRaycasts = enable;
    }

    private void OnDestroy()
    {
        startButton.onClick.RemoveAllListeners();
        
        logoutButton.onClick.RemoveAllListeners();
    }
}
