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

        playerDisplayName.text = FirebaseAuthMgr.user.DisplayName;
    }

    private void OnDestroy()
    {
        homeButton.onClick.RemoveListener(() => SceneControl.Instance.GoToMain());
    }
}
