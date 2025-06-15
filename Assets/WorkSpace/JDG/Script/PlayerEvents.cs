using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerEvents
{
    public static event Action<float, float> OnHPChanged;
    public static event Action<float, float> OnFuelChanged;

    public static void ChangeHP(float maxHP, float currentHP)
    {
        OnHPChanged?.Invoke(maxHP, currentHP);
    }

    public static void ChangeFuel(float maxFuel, float currenteFuel)
    {
        OnFuelChanged?.Invoke(maxFuel, currenteFuel);
    }
}
