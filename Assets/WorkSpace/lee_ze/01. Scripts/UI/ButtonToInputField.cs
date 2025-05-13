using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonToInputField : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField email;

    [SerializeField]
    private TMP_InputField password;

    [SerializeField]
    private TMP_InputField nickname;

    [SerializeField]
    private TextMeshProUGUI num;

    private int i;

    private void Start()
    {
        i = 0;

        num.text = $"{i}";
    }

    public void ClearText()
    {
        num.text = null;

        email.text = null;

        password.text = null;

        nickname.text = null;
    }

    public void IncreaseI()
    {
        i++;

        num.text = $"{i}";
    }

    public void DecreaseI()
    {
        i--;

        if (i < 0)
        {
            i = 0;
        }

        num.text = $"{i}";
    }

    public void SetAcount_qwe()
    {
        email.text = "qwe@qwe.com";

        password.text = "qweqwe";
    }

    public void CreateAcount()
    {
        email.text = "qwe" + i + "@qwe.com";

        password.text = "qweqwe";

        nickname.text = "user" + i;
    }
}
