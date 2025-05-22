using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    [SerializeField]
    private GameObject stationCanvas;

    [SerializeField]
    private GameObject worldMap;

    [SerializeField]
    private Button toWorldMapButton;

    private void Start()
    {
        stationCanvas.SetActive(true);

        worldMap.SetActive(false);

        toWorldMapButton = stationCanvas.transform.Find("World Map Button")?.GetComponent<Button>();

        toWorldMapButton.onClick.AddListener(() =>
        {
            worldMap.SetActive(true);

            stationCanvas.SetActive(false);
        });
    }
}
