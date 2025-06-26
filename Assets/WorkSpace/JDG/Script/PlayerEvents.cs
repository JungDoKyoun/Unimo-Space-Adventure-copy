using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerEvents
{
    public static event Action<float, float> _OnHPChanged;
    public static event Action<float, float> _OnFuelChanged;
    public static event Action _OnCurrencyChanged;
    public static event Action _OnRelicChanged;

    public static void ChangeHP(float maxHP, float currentHP)
    {
        _OnHPChanged?.Invoke(maxHP, currentHP);
    }

    public static void ChangeFuel(float maxFuel, float currenteFuel)
    {
        _OnFuelChanged?.Invoke(maxFuel, currenteFuel);
    }

    public static void ChangeCurrency()
    {
        Debug.Log("ÀÚ¿ø ¹Ù²ñ");
        _OnCurrencyChanged?.Invoke();
    }

    public static void ChangeRelic()
    {
        _OnRelicChanged?.Invoke();
    }
}
