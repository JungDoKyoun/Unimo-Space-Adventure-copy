using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetCurrency : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField rewardIngameCurrencyInputField;

    [SerializeField]
    private TMP_InputField rewardMetaCurrencyInputField;

    private int ingameCurrency;

    private int metaCurrency;

    private void Start()
    {
        ingameCurrency = 0;

        metaCurrency = 0;

        SetFieldZero();
    }

    private void SetFieldZero()
    {
        rewardIngameCurrencyInputField.text = 0.ToString();

        rewardMetaCurrencyInputField.text = 0.ToString();
    }

    public void IncreaseIngameCurrency()
    {
        ingameCurrency++;

        rewardIngameCurrencyInputField.text = $"{ingameCurrency}";
    }

    public void DecreaseIngameCurrency()
    {
        ingameCurrency--;

        rewardIngameCurrencyInputField.text = $"{ingameCurrency}";
    }

    public void IncreaseMetaCurrency()
    {
        metaCurrency++;

        rewardMetaCurrencyInputField.text = $"{metaCurrency}";
    }

    public void DecreaseMetaCurrency()
    {
        metaCurrency--;

        rewardMetaCurrencyInputField.text = $"{metaCurrency}";
    }
}
