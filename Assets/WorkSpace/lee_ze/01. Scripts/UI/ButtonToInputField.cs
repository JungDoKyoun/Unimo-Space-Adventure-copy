using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonToInputField : MonoBehaviour
{
    [Header("E-mail")]
    [SerializeField]
    private TextMeshProUGUI savedEmail;

    [SerializeField]
    private TMP_InputField email;

    [Header("Password")]
    [SerializeField]
    private TextMeshProUGUI savedPassword;

    [SerializeField]
    private TMP_InputField password;



    public void SetEmail()
    {
        email.text = savedEmail.text;
    }

    public void SetPassword()
    {
        password.text = savedPassword.text;
    }
}
