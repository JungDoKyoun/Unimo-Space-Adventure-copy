using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProfileMgr : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI playerDisplayName;

    [SerializeField]
    private TextMeshProUGUI playerWinningRate;

    private Button homeButton;

    private void Start()
    {
        homeButton = GameObject.Find("Home Button").GetComponent<Button>();

        homeButton.onClick.AddListener(() => SceneControl.Instance.GoToMain());

        playerDisplayName.text = FirebaseAuthMgr.User.DisplayName;

        SetupProfile();
    }

    private void SetupProfile()
    {
        playerWinningRate.text = 
            FirebaseDataBaseMgr.PlayCount.ToString("F0") + "��" + " / " 
            + FirebaseDataBaseMgr.WinCount.ToString("F0") + "��" + " / " 
            + $"{(FirebaseDataBaseMgr.PlayCount - FirebaseDataBaseMgr.WinCount).ToString("F0")}" + "��" 
            + "\n" + FirebaseDataBaseMgr.WinningRate.ToString("F1") + "%"; // �Ҽ��� 1�ڸ� ǥ��
    }

    private void OnDestroy()
    {
        homeButton.onClick.RemoveListener(() => SceneControl.Instance.GoToMain());
    }
}
