using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhereToGo : MonoBehaviour
{
    [SerializeField]
    private Button worldMapButton;

    private GameObject worldMap;

    private void Start()
    {
        worldMap = GameObject.Find("World Map");

        worldMapButton.onClick.AddListener(() => GoToWorldMap());
    }

    private void GoToWorldMap()
    {
        worldMap.SetActive(true);

        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        worldMapButton.onClick.RemoveListener(() => GoToWorldMap());
    }
}
