using JDG;
using UnityEngine;

/// <summary>
/// 스테이지 클리어 여부(성공/실패)에 따라 보여지는 UI 관리
/// </summary>
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
