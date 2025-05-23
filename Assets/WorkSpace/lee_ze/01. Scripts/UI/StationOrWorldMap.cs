using JDG;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class StationOrWorldMap : MonoBehaviour
{
    private GameObject station;

    private GameObject worldMap;

    private void Start()
    {
        station = GameObject.Find("Station Canvas");

        worldMap = GameObject.Find("World Map");

        UISettings();
    }

    private void UISettings()
    {
        bool isClear = GameStateManager.IsClear;

        station.SetActive(!isClear);

        worldMap.SetActive(isClear);
    }
}
