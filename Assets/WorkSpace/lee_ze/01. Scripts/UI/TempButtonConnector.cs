using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempButtonConnector : MonoBehaviour
{
    public Button goToRegisterButton;

    private void Start()
    {
        goToRegisterButton.onClick.AddListener(() => SceneControl.Instance.GoToRegister());
    }

    private void OnDisable()
    {
        goToRegisterButton.onClick.RemoveListener(() => SceneControl.Instance.GoToRegister());
    }
}
